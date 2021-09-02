using EFPractice.Database;
using EFPractice.ProcessControl;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace EFPractice.Database {
  class FSSContext : DbContext {    
    public DbSet<ImageScoreListDetail> ImageScoreListDetail { get; set; }

    private CredentialManager _credential;
    private SshClient _client;
    private ForwardedPortLocal _tunnel;
    private ProcessManager _process;

    internal FSSContext() {
      _credential = new CredentialManager();
      _process = new ProcessManager();

      _process.KillProcessByPortNumber(_credential.DBLocalPort);
      ConnectToClient();
      ConnectToPortForwarding();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      if (!optionsBuilder.IsConfigured) {
        optionsBuilder.UseNpgsql(
        $"Host={ _credential.DBLocalServer };" +
        $"Database={ _credential.DBName };" +
        $"Username={ _credential.DBUsername };" +
        $"Password={ _credential.DBPassword }");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("public");
      base.OnModelCreating(modelBuilder);
    }

    internal void Test() {
      _process.KillProcessByPortNumber(_credential.DBLocalPort);
      ConnectToClient();
      ConnectToPortForwarding();
    }

    private void ConnectToClient() {
      _client = new SshClient(
        _credential.DBTunnelHost,
        _credential.DBTunnelPort,
        _credential.DBTunnelUser,
        _credential.DBTunnelPassword);
      _client.Connect();
      if (!_client.IsConnected) throw new Exception("Client connection failed.");
    }

    private void ConnectToPortForwarding() {
      _tunnel = new ForwardedPortLocal(
       _credential.DBLocalServer,
       (uint)_credential.DBLocalPort,
       _credential.DBLocalServer,
       (uint)_credential.DBLocalPort);

      _client.AddForwardedPort(_tunnel);
      _tunnel.Start();
      if (!_tunnel.IsStarted) throw new Exception("Port Forwarding connection failed.");
    }
  }
}

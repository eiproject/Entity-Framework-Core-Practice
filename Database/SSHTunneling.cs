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
  class SSHTunneling {
    private ProcessManager _process;
    private SshClient _client;
    private ForwardedPortLocal _tunnel;
    private CredentialManager _credential;

    internal SSHTunneling() {
      _process = new ProcessManager();
      _credential = new CredentialManager();
    }

    internal void StartTunneling() {
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

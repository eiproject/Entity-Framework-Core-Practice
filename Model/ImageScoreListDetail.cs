using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFPractice.Model {
  class ImageScoreListDetail : DbContext {
    public int Id { get; set; }
    public int ScorerId { get; set; }
    public int ScorerValue { get; set; }
    public string Timing { get; set; }
    public string ScTimestamp { get; set; }
    public int? ScoreFormat { get; set; }

    private CredentialManager _credential;

    ImageScoreListDetail() {
      _credential = new CredentialManager();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseNpgsql(
        $"Host={ _credential.DBTunnelHost };" +
        $"Database={ _credential.DBName };" +
        $"Username={ _credential.DBUsername };" +
        $"Password={_credential.DBPassword }");
  }
}

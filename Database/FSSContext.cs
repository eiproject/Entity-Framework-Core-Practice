using Microsoft.EntityFrameworkCore;

namespace EFPractice.Database {
  class FSSContext : DbContext {
    public DbSet<ImageScoreListDetail> ImageScoreListDetail { get; set; }

    private CredentialManager _credential;
    private SSHTunneling _SSHTunneling;

    internal FSSContext() {
      _credential = new CredentialManager();
      _SSHTunneling = new SSHTunneling();
      _SSHTunneling.StartTunneling();
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

/*    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.HasDefaultSchema("public");
      base.OnModelCreating(modelBuilder);
    }*/
  }
}

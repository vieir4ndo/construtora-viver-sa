using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConstrutoraViverSA.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());
        public DbSet<Obra> Obra { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<Material> Material { get; set; }
        
        public DbSet<ObraMaterial> ObraMaterial { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=construtora-viver-sa;User Id=postgres;Password=secret;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
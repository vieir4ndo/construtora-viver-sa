using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConstrutoraViverSA.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());
        public DbSet<Obra> Obras { get; set; }
        
        public DbSet<ObraFuncionarios> ObraFuncionarios { get; set; }
        
        public DbSet<ObraMateriais> ObraMateriais { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<Material> Materiais { get; set; }

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
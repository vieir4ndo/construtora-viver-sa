using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConstrutoraViverSA.Infraestrutura
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
                .UseSqlServer("Data Source=localhost,1433;Initial Catalog=construtora-viver-sa;Integrated Security=False;User Id=sa;Password=Stone@2022!;Persist Security Info=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
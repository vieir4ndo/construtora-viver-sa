using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConstrutoraViverSA.Infraestrutura
{
    public class ApplicationContext : DbContext
    {
        public IConfiguration Configuration { get; }
        
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<Material> Materiais { get; set; }

        public ApplicationContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer(Configuration["Database:ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}

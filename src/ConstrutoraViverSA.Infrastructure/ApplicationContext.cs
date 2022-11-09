using System.Diagnostics.CodeAnalysis;
using System.IO;
using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConstrutoraViverSA.Infrastructure;

[ExcludeFromCodeCoverage]
public class ApplicationContext : DbContext
{
    public DbSet<Obra> Obra { get; set; }
    public DbSet<Funcionario> Funcionario { get; set; }
    public DbSet<Orcamento> Orcamento { get; set; }
    public DbSet<Material> Material { get; set; }
    public DbSet<ObraMaterial> ObraMaterial { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}/../ConstrutoraViverSA.Api")
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration["ConnectionString:Postgres"];
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}
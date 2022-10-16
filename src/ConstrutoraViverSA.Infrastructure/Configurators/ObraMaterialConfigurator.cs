using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraViverSA.Infrastructure.Configurators;

public class ObraMaterialConfigurator : IEntityTypeConfiguration<ObraMaterial>
{
    public void Configure(EntityTypeBuilder<ObraMaterial> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(a => a.MaterialId).IsRequired();
        builder.Property(a => a.ObraId).IsRequired();
        builder.Property(a => a.DataHora).IsRequired();
        builder.Property(a => a.Operacao).IsRequired();

        builder.HasOne<Material>(c => c.Material).WithMany(c => c.ObraMateriais).HasForeignKey(c => c.MaterialId);
        builder.HasOne<Obra>(c => c.Obra).WithMany(c => c.ObraMateriais).HasForeignKey(c => c.ObraId);
    }
}
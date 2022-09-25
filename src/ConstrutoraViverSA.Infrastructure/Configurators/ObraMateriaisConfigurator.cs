using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraViverSA.Infrastructure.Configurators
{
    public class ObraMateriaisConfigurator : IEntityTypeConfiguration<ObraMaterial>
    {
        public void Configure(EntityTypeBuilder<ObraMaterial> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => new { p.MaterialId, p.ObraId }).IsUnique();
            builder.Property(a => a.MaterialId).IsRequired();
            builder.Property(a => a.ObraId).IsRequired();

            builder.HasOne<Material>(c => c.Material).WithMany(c => c.ObraMateriais).HasForeignKey(c => c.MaterialId);
            builder.HasOne<Obra>(c => c.Obra).WithMany(c => c.ObraMateriais).HasForeignKey(c => c.ObraId);
        }
    }
}
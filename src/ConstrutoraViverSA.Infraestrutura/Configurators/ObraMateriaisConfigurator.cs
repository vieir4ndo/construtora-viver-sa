using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraViverSA.Infraestrutura.Configurators
{
    public class ObraMateriaisConfigurator : IEntityTypeConfiguration<ObraMateriais>
    {
        public void Configure(EntityTypeBuilder<ObraMateriais> builder)
        {
            builder.HasKey(p => new { p.MaterialId, p.ObraId });
            builder.Property(a => a.MaterialId)
                .IsRequired();
            builder.Property(a => a.ObraId)
                .IsRequired();

            builder.HasOne<Material>(c => c.Material).WithMany(c => c.ObraMateriais).HasForeignKey(c => c.MaterialId);

            builder.HasOne<Obra>(c => c.Obra).WithMany(c => c.ObraMateriais).HasForeignKey(c => c.ObraId);
        }
    }
}
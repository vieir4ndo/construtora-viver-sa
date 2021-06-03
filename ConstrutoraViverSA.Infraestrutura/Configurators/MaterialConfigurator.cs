using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraViverSA.Infraestrutura.Configurators
{
    public class MaterialConfigurator : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasMaxLength(80).IsRequired(false);
            builder.Property(p => p.Descricao).IsRequired(false);
            builder.Property(p => p.Tipo).IsRequired(false);
            builder.Property(p => p.Valor).IsRequired(false);
            builder.Property(p => p.DataValidade).IsRequired(false);
        }
    }
}

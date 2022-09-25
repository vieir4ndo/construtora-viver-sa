using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraViverSA.Infrastructure.Configurators
{
    public class MaterialConfigurator : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasMaxLength(80).IsRequired();
            builder.Property(p => p.Descricao).IsRequired();
            builder.Property(p => p.Tipo).IsRequired();
            builder.Property(p => p.Valor).IsRequired();
            builder.Property(p => p.Quantidade).IsRequired().HasDefaultValue(0);
        }
    }
}

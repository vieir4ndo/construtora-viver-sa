using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraViverSA.Infrastructure.Configurators
{
    public class EstoqueConfigurator: IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Quantidade).IsRequired();
            builder.Property(p => p.Operacao).IsRequired();
            builder.Property(a => a.MaterialId).IsRequired();

            builder.HasOne<Material>(c => c.Material).WithMany(c => c.Estoque).HasForeignKey(c => c.MaterialId);
        }
    }
}
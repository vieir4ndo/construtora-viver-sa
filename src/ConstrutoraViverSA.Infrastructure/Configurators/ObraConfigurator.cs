using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraViverSA.Infrastructure.Configurators
{
    public class ObraConfigurator : IEntityTypeConfiguration<Obra>
    {
        public void Configure(EntityTypeBuilder<Obra> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasMaxLength(80).IsRequired(false);
            builder.Property(p => p.Endereco).IsRequired(false);
            builder.Property(p => p.TipoObra).IsRequired(false);
            builder.Property(p => p.Descricao).IsRequired(false);
            builder.Property(p => p.Valor).IsRequired(false);
            builder.Property(p => p.PrazoConclusao).IsRequired(false);
        }
    }
}

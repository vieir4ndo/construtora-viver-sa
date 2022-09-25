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
            builder.Property(p => p.Nome).HasMaxLength(80).IsRequired();
            builder.Property(p => p.Endereco).IsRequired();
            builder.Property(p => p.TipoObra).IsRequired();
            builder.Property(p => p.Descricao).IsRequired();
            builder.Property(p => p.Valor).IsRequired();
            builder.Property(p => p.PrazoConclusao).IsRequired();
        
            builder.HasOne<Orcamento>(c => c.Orcamento).WithOne(c => c.Obra).HasForeignKey<Obra>(c => c.OrcamentoId);
            builder.HasMany<Funcionario>(c => c.Funcionarios).WithMany(c => c.Obras);
        }
    }
}

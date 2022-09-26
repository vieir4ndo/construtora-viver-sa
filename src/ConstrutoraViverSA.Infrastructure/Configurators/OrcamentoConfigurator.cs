using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraViverSA.Infrastructure.Configurators;

public class OrcamentoConfigurator : IEntityTypeConfiguration<Orcamento>
{
    public void Configure(EntityTypeBuilder<Orcamento> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Descricao).IsRequired();
        builder.Property(p => p.Endereco).IsRequired();
        builder.Property(p => p.TipoObra).IsRequired();
        builder.Property(p => p.DataEmissao).IsRequired();
        builder.Property(p => p.DataValidade).IsRequired();
        builder.Property(p => p.Valor).IsRequired();
    }
}
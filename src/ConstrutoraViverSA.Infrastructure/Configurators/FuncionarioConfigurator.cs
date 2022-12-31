using System.Diagnostics.CodeAnalysis;
using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraViverSA.Infrastructure.Configurators;

[ExcludeFromCodeCoverage]
public class FuncionarioConfigurator : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome).HasMaxLength(80).IsRequired();
        builder.Property(p => p.Cpf).HasMaxLength(11).IsRequired();
        builder.Property(p => p.DataNascimento).IsRequired();
        builder.Property(p => p.Genero).IsRequired();
        builder.Property(p => p.NumCtps).IsRequired();
        builder.Property(p => p.Endereco).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Telefone).IsRequired();
        builder.Property(p => p.Cargo).IsRequired();
    }
}
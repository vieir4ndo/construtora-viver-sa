using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraViverSA.Infrastructure.Configurators
{
    public class FuncionarioConfigurator : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasMaxLength(80).IsRequired(false);
            builder.Property(p => p.Cpf).HasMaxLength(11).IsRequired(false);
            builder.Property(p => p.DataNascimento).IsRequired(false);
            builder.Property(p => p.Genero).IsRequired(false);
            builder.Property(p => p.NumCtps).IsRequired(false);
            builder.Property(p => p.Endereco).IsRequired(false);
            builder.Property(p => p.Email).IsRequired(false);
            builder.Property(p => p.Telefone).IsRequired(false);
            builder.Property(p => p.Cargo).IsRequired(false);
        }
    }
}

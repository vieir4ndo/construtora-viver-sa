using ConstrutoraViverSA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstrutoraViverSA.Infraestrutura.Configurators
{
    public class ObraFuncionariosConfigurator : IEntityTypeConfiguration<ObraFuncionarios>
    {
        public void Configure(EntityTypeBuilder<ObraFuncionarios> builder)
        {
            builder.HasKey(p => new { p.FuncionarioId, p.ObraId });
            builder.Property(a => a.FuncionarioId)
                .IsRequired();
            builder.Property(a => a.ObraId)
                .IsRequired();

            builder.HasOne<Funcionario>(c => c.Funcionario).WithMany(c => c.ObraFuncionarios)
                .HasForeignKey(c => c.FuncionarioId);

            builder.HasOne<Obra>(c => c.Obra).WithMany(c => c.ObraFuncionarios).HasForeignKey(c => c.ObraId);
        }
    }
}
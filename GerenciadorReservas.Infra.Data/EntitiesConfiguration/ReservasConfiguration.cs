using GerenciadorReservas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorReservas.Infra.Data.EntitiesConfiguration
{
    public class ReservasConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.DataHora)
                   .IsRequired();

            builder.Property(r => r.Status)
                   .HasConversion<int>() // Armazenando o enum como int
                   .IsRequired();

            // Relacionamento com Sala
            builder.HasOne(r => r.Sala)
                   .WithMany(s => s.Reservas)
                   .HasForeignKey(r => r.SalaId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Usuário
            builder.HasOne(r => r.Usuario)
                   .WithMany(u => u.Reservas)
                   .HasForeignKey(r => r.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

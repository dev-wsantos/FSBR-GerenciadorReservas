using GerenciadorReservas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorReservas.Infra.Data.Data.EntitiesConfiguration
{
    public class ReservasConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.DataHoraInicio)
                   .IsRequired();

            builder.Property(r => r.DataHoraFim)
                   .IsRequired();

            builder.Property(r => r.Status)
                   .HasConversion<int>() 
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

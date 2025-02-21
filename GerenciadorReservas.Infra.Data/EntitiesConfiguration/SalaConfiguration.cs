using GerenciadorReservas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace GerenciadorReservas.Infra.Data.EntitiesConfiguration
{
    public class SalaConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(s => s.Nome)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasMany(s => s.Reservas)
                   .WithOne(r => r.Sala)
                   .HasForeignKey(r => r.SalaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using GeneralNotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneralNotes.Infrastructure.TypeConfiguration;
public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("Exercises");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(x => x.Type)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(x => x.Level)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(x => x.Charge)
            .IsRequired()
            .HasColumnType("double");

        builder.Property(x => x.Repetitions)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(x => x.Stop)
            .HasColumnType("int");

        builder.Property(x => x.Equipment)
            .HasColumnType("varchar(100)");

        builder.Property(x => x.Details)
            .HasColumnType("varchar(350)");
    }
}

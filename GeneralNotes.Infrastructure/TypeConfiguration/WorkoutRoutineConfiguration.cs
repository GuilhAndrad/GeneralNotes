using GeneralNotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneralNotes.Infrastructure.TypeConfiguration;
public class WorkoutRoutineConfiguration : IEntityTypeConfiguration<WorkoutRoutine>
{
    public void Configure(EntityTypeBuilder<WorkoutRoutine> builder)
    {
        builder.ToTable("WorkoutRoutines");

        builder.Property(x => x.Name)
           .IsRequired()
           .HasColumnType("varchar(50)");

        builder.Property(x => x.DayOfWeek)
            .HasColumnType("int")
            .IsRequired();
    }
}

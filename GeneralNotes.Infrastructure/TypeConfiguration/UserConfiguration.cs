using GeneralNotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneralNotes.Infrastructure.TypeConfiguration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(x => x.Password)
            .IsRequired().
            HasColumnType("varchar(100)");

        builder.Property(x => x.Contact)
           .IsRequired().
           HasColumnType("varchar(14)");
    }
}
using Curiosity.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curiosity.Infrastructure.Configurations;

internal sealed class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.Property(r => r.Id)
            .HasColumnName("id")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(user => user.Id)
            .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(user => user.Name)
            .HasMaxLength(200)
            .HasConversion(name => name.Value, value => new Name(value));

        builder.Property(user => user.Email)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, value => new Email(value));

        builder.Property(user => user.Status)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, value => new Status(value));

        builder.HasIndex(user => user.Email).IsUnique();

        builder.HasIndex(user => user.IdentityId).IsUnique();
    }
}

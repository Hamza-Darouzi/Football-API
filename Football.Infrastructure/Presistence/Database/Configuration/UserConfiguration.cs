
namespace Football.Infrastructure.Database.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //Properties
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

        builder.Property(x => x.Username)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.ResetToken)
               .HasMaxLength(10)
               .IsRequired(false);

        // Relationship 1 User -> Many Tokens
        builder.HasMany(x => x.RefreshTokens)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(true);

        builder.HasIndex(x => x.Username)
               .IsUnique(true);

        builder.ToTable(nameof(User));

    }

}

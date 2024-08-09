
namespace Football.EF.Data.Config;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //Properties
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1)
                .ValueGeneratedOnAdd();

        builder.Property(x => x.Username)
               .HasColumnType("nvarchar")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.PasswordHash)
               .HasColumnType("nvarchar")
               .HasMaxLength(250)
               .IsRequired();
        builder.ComplexProperty(x => x.RefreshToken, b =>
        {
            b.Property(r => r.Token)
             .HasColumnType("nvarchar(MAX)");
            b.Property(r => r.ExpireAt)
            .HasColumnType("datetime2");
        });

        builder.HasIndex(x => x.Username)
               .IsUnique(true);

        builder.ToTable(nameof(User));

    }

}

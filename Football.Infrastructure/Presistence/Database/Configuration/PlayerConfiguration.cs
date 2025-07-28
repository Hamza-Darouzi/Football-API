
namespace Football.Infrastructure.Database.Configuration;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        //Properties
        builder.HasKey(x=>x.Id);
        builder.Property(x => x.Id)
               .UseIdentityColumn()
               .ValueGeneratedOnAdd();

        builder.Property(x=>x.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x=>x.Nation)
               .HasMaxLength(100)
               .IsRequired();
        
        builder.Property(x => x.BirthDay)
               .HasColumnType("datetime2");

        builder.HasIndex(x => x.Name);

        // Relationship 1 Clubs -> Many Players
        builder.HasOne(x=>x.Club)
                .WithMany(x=>x.Players)
                .HasForeignKey(x=>x.ClubId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

        builder.HasIndex(p=>p.ClubId);
        builder.ToTable(nameof(Player));
        
        // Sample Data
        builder.HasData(
            new[]
            { 
                new Player{ Id=1, Name="Lioniel Messi"    ,Nation="Argentina", BirthDay = new DateTime(1987,06,24), ClubId=1 },
                new Player{ Id=2, Name="Andres Iniesta"   ,Nation="Spain",     BirthDay = new DateTime(1984,05,11), ClubId=1 },
                new Player{ Id=3, Name="Xavi Hernandez"   ,Nation="Spain",     BirthDay = new DateTime(1980,01,25), ClubId=1 },
                new Player{ Id=4, Name="Cristiano Ronaldo",Nation="Portugal",  BirthDay = new DateTime(1985,02,05), ClubId=2 },
                new Player{ Id=5, Name="Karim Benzema"    ,Nation="France",    BirthDay = new DateTime(1987,12,19), ClubId=2 },
                new Player{ Id=6, Name="KAKA"             ,Nation="Brazil",    BirthDay = new DateTime(1982,04,02), ClubId=2 },
            }
        );


    }
}
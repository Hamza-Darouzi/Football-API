
namespace Football.EF.Data.Config;

public class PlayerConfig : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        //Properties
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id).HasColumnType("int").UseIdentityColumn(1,1).ValueGeneratedOnAdd().IsRequired();
        builder.Property(x=>x.Name).HasMaxLength(100).HasColumnType("nvarchar").IsRequired();
        builder.Property(x=>x.Nation).HasMaxLength(100).HasColumnType("nvarchar").IsRequired();
        
        builder.Property<DateOnly>(x => x.BirthYear)
               .HasConversion<DateOnlyConverter, DateOnlyComparer>()
               .HasColumnType("date");
        
        //Relationship 1 Clubs -> Many Players
        builder.HasOne(x=>x.Club)
                .WithMany(x=>x.Players)
                .HasForeignKey(x=>x.ClubId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

        builder.ToTable(nameof(Player));
        
        // Sample Data
        builder.HasData(
            new[]
            { 
                new Player{ Id=1, Name="Lioniel Messi"    ,Nation="Argentina", BirthYear = new DateOnly(1987,06,24), ClubId=1 },
                new Player{ Id=2, Name="Andres Iniesta"   ,Nation="Spain",     BirthYear = new DateOnly(1984,05,11), ClubId=1 },
                new Player{ Id=3, Name="Xavi Hernandez"   ,Nation="Spain",     BirthYear = new DateOnly(1980,01,25), ClubId=1 },
                new Player{ Id=4, Name="Cristiano Ronaldo",Nation="Portugal",  BirthYear = new DateOnly(1985,02,05), ClubId=2 },
                new Player{ Id=5, Name="Karim Benzema"    ,Nation="France",    BirthYear = new DateOnly(1987,12,19), ClubId=2 },
                new Player{ Id=6, Name="KAKA"             ,Nation="Brazil",    BirthYear = new DateOnly(1982,04,02), ClubId=2 },
            }
        );


    }
}
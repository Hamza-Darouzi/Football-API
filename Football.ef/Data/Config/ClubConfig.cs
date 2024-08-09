
namespace Football.EF.Data.Config;

public class ClubConfig : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        // Properties
        builder.HasKey(x=>x.Id);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x=>x.Name)
               .HasMaxLength(100)
               .HasColumnType("nvarchar")
               .IsRequired();

        builder.Property(x => x.FoundingDate)
               .HasConversion<DateOnlyConverter, DateOnlyComparer>()
               .HasColumnType("date")
               .IsRequired(false);

        builder.HasIndex(x => x.Name);
        builder.ToTable(nameof(Club));
        
        //Relationship 1 League -> Many Clubs

        builder.HasOne(x=>x.League)
               .WithMany(x=>x.Clubs)
               .HasForeignKey(x=>x.LeagueId)
               .OnDelete(DeleteBehavior.SetNull)
               .IsRequired(false);

        // Sample Data
        builder.HasData(
            new[]
            {
                new Club {Id=1,Name="Fc Barcelona", FoundingDate=new DateOnly(1899,11,29),LeagueId=1},
                new Club {Id=2,Name="Real Madrid" , FoundingDate=new DateOnly(1902,02,06)  ,LeagueId=1},
                new Club {Id=3,Name="PSG" ,LeagueId=3},
                new Club {Id=4,Name="Napoli",LeagueId=4},
                new Club {Id=5,Name="Manchester City",LeagueId=2},
                new Club {Id=6,Name="Liverpool",LeagueId=2}
            }
        );
    }

    
}

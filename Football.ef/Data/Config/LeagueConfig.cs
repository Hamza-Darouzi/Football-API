
namespace Football.EF.Data.Config;

public class LeagueConfig : IEntityTypeConfiguration<League>
{
    public void Configure(EntityTypeBuilder<League> builder)
    {
        //Properties
        builder.HasKey(x=>x.Id);
        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x=>x.Name)
               .HasColumnType("nvarchar")
               .HasMaxLength(100)
               .IsRequired();
       
        builder.HasIndex(x => x.Name);
        builder.ToTable(nameof(League));

        //Sample Data
        builder.HasData(
            new[]
            {
               new League{Id=1,Name="La Liga"        },
               new League{Id=2,Name="Premiere League"} ,
               new League{Id=3,Name="League 1"} ,
               new League{Id=4,Name="Serie A"} ,
            }
        );

    }

}

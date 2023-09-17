
namespace Football.EF.Data.Config;

public class LeagueConfig : IEntityTypeConfiguration<League>
{
    public void Configure(EntityTypeBuilder<League> builder)
    {
        //Properties
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id).HasColumnType("int").UseIdentityColumn(1,1).ValueGeneratedOnAdd().IsRequired();
        builder.Property(x=>x.Name).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
       
        builder.ToTable(nameof(League));

        //Sample Data
        builder.HasData(
            new[]
            {
               new League{Id=1,Name="La Liga"        },
               new League{Id=2,Name="Premiere League"} 
            }
        );

    }

}

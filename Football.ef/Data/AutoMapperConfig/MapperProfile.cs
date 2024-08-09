


namespace Football.EF.Data.AutoMapperConfig;

internal class MapperProfile:Profile
{
    public MapperProfile()
    {

        CreateMap<CreateClubDTO, Club>()
           .ForMember(dist => dist.Name, src => src.MapFrom(x => x.name))
           .ForMember(dist => dist.FoundingDate, src => src.MapFrom(x => x.foundingDate))
           .ForMember(dist => dist.LeagueId, src => src.MapFrom(x => x.leagueId))
           .ReverseMap();

        CreateMap<CreateLeagueDTO, League>()
            .ForMember(dist => dist.Name, src => src.MapFrom(x => x.name))
            .ReverseMap();

        CreateMap<CreatePlayerDTO, Player>()
            .ForMember(dist => dist.Name, src => src.MapFrom(x => x.name))
            .ForMember(dist => dist.Nation, src => src.MapFrom(x => x.nation))
            .ForMember(dist => dist.BirthYear, src => src.MapFrom(x => x.birthYear))
            .ForMember(dist => dist.ClubId, src => src.MapFrom(x => x.clubId))
            .ReverseMap();

    }

}

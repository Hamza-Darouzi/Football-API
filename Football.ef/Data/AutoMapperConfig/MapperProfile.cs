

namespace Football.EF.Data.AutoMapperConfig
{
    internal class MapperProfile:Profile
    {
        public MapperProfile()
        {

            CreateMap<ClubDTO, Club>()
                .ForMember(dist => dist.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dist => dist.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dist => dist.FoundingDate, src => src.MapFrom(x => x.FoundingDate))
                .ForMember(dist => dist.LeagueId, src => src.MapFrom(x => x.LeagueId))
                .ReverseMap();

            CreateMap<LeagueDTO, League>()
                .ForMember(dist => dist.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dist => dist.Name, src => src.MapFrom(x => x.Name))
                .ReverseMap();

            CreateMap<PlayerDTO, Player>()
                .ForMember(dist => dist.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dist => dist.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dist => dist.Nation, src => src.MapFrom(x => x.Nation))
                .ForMember(dist => dist.BirthYear, src => src.MapFrom(x => x.BirthYear))
                .ForMember(dist => dist.ClubId, src => src.MapFrom(x => x.ClubId))
                .ReverseMap();
        }

    }
}

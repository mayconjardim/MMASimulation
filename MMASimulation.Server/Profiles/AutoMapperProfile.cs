using AutoMapper;
using MMASimulation.Shared.Dtos.Fighters;
using MMASimulation.Shared.Models.Fighters;

namespace MMASimulation.Server.Profiles
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {

            //Fighters
            CreateMap<Fighter, FighterDto>().ReverseMap();
            CreateMap<Fighter, FighterCreateDto>().ReverseMap();
            CreateMap<FighterRatings, FighterRatingsDto>().ReverseMap();
            CreateMap<FighterStrategies, FighterStrategiesDto>().ReverseMap();
            CreateMap<FighterStyles, FighterStylesDto>().ReverseMap();

        }

    }
}

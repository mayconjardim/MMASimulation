using AutoMapper;
using MMASimulation.Shared.Dtos.Fighters;
using MMASimulation.Shared.Dtos.Fights;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

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

            //Fights
            CreateMap<Fight, FightDto>().ReverseMap();
            CreateMap<Fight, FightCreateDto>().ReverseMap();

        }

    }
}

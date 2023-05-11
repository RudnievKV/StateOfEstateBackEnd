using AutoMapper;
using MonteNegRo.Dtos.Local_CityDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class Local_CityProfile : Profile
    {
        public Local_CityProfile()
        {
            CreateMap<Local_City, Local_CityDto>();
        }
    }
}

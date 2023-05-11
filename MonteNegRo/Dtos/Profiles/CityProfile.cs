using AutoMapper;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>();
        }
    }
}

using AutoMapper;
using MonteNegRo.Dtos.City_PropertyDtos;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class City_PropertyProfile : Profile
    {
        public City_PropertyProfile()
        {
            CreateMap<City_Property, City_PropertyDto>();
        }
    }
}

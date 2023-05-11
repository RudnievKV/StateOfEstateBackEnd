using AutoMapper;
using MonteNegRo.Dtos.Benefit_PropertyDtos;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class Benefit_PropertyProfile : Profile
    {
        public Benefit_PropertyProfile()
        {
            CreateMap<Benefit_Property, Benefit_PropertyDto>();
        }
    }
}

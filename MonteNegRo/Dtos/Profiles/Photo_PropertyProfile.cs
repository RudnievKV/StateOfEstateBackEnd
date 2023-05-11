using AutoMapper;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.Photo_PropertyDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class Photo_PropertyProfile : Profile
    {
        public Photo_PropertyProfile()
        {
            CreateMap<Photo_Property, Photo_PropertyDto>();
        }
    }
}

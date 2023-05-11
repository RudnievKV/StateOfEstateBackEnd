using AutoMapper;
using MonteNegRo.Dtos.BenefitDtos;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.Local_BenefitDtos;
using MonteNegRo.Dtos.Local_CityDtos;
using MonteNegRo.Dtos.Local_PropertyDtos;
using MonteNegRo.Dtos.PhotoDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Models;
using System.Collections.Generic;
using System.Linq;

namespace MonteNegRo.Dtos.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<City_Property, CityDto>()
                .ForMember(s => s.Local_Cities, opt => opt.MapFrom(src => src.City.Local_Cities));
            //.ForMember(s => s.Neighborhoods, opt => opt.MapFrom(src => src.City.Neighborhoods));


            CreateMap<Photo_Property, PhotoDto>()
                .ForMember(s => s.PhotoUrl, opt => opt.MapFrom(src => src.Photo.PhotoUrl))
                .ForMember(s => s.PhotoName, opt => opt.MapFrom(src => src.Photo.PhotoName));

            CreateMap<Benefit_Property, BenefitDto>()
                .ForMember(s => s.Local_Benefits, opt => opt.MapFrom(src => src.Benefit.Local_Benefits));


            CreateMap<Property, PropertyDto>()
                .ForMember(s => s.Cities, opt => opt.MapFrom(src => src.City_Properties))
                .ForMember(s => s.Photos, opt => opt.MapFrom(src => src.Photo_Properties))
                .ForMember(s => s.Benefits, opt => opt.MapFrom(src => src.Benefit_Properties));


        }
    }
}

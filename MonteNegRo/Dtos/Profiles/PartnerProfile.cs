using AutoMapper;
using MonteNegRo.Dtos.AdvertisementSettingDtos;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PartnerDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class PartnerProfile : Profile
    {
        public PartnerProfile()
        {
            CreateMap<Partner_City, CityDto>();
            CreateMap<Partner, PartnerDto>()
                .ForMember(s => s.Cities, opt => opt.MapFrom(src => src.Partner_Cities));
        }
    }
}

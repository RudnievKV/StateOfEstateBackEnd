using AutoMapper;
using MonteNegRo.Dtos.AdvertisementSettingDtos;
using MonteNegRo.Dtos.Partner_CityDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class Partner_CityProfile : Profile
    {
        public Partner_CityProfile()
        {
            CreateMap<Partner_City, Partner_CityDto>();
        }
    }
}

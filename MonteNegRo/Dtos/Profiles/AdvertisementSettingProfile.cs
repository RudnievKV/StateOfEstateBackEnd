using AutoMapper;
using MonteNegRo.Dtos.AdvertisementSettingDtos;
using MonteNegRo.Dtos.Benefit_PropertyDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class AdvertisementSettingProfile : Profile
    {
        public AdvertisementSettingProfile()
        {
            CreateMap<AdvertisementSetting, AdvertisementSettingDto>();
        }
    }
}

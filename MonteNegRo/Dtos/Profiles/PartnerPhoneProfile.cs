using AutoMapper;
using MonteNegRo.Dtos.AdvertisementSettingDtos;
using MonteNegRo.Dtos.PartnerPhoneDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class PartnerPhoneProfile : Profile
    {
        public PartnerPhoneProfile()
        {
            CreateMap<PartnerPhone, PartnerPhoneDto>();
        }
    }
}

using AutoMapper;
using MonteNegRo.Dtos.AdvertisementSettingDtos;
using MonteNegRo.Dtos.RealticAccountDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class RealticAccountProfile : Profile
    {
        public RealticAccountProfile()
        {
            CreateMap<RealticAccount, RealticAccountDto>();
        }
    }
}

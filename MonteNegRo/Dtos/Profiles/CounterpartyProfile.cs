using AutoMapper;
using MonteNegRo.Dtos.AdvertisementSettingDtos;
using MonteNegRo.Dtos.CounterpartyDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class CounterpartyProfile : Profile
    {
        public CounterpartyProfile()
        {
            CreateMap<Property, PropertyIdAndPhoto>();
            CreateMap<Counterparty, CounterpartyDto>();
        }
    }
}

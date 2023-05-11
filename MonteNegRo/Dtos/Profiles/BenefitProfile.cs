using AutoMapper;
using MonteNegRo.Dtos.BenefitDtos;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class BenefitProfile : Profile
    {
        public BenefitProfile()
        {
            CreateMap<Benefit, BenefitDto>();
            //CreateMap<typeof(Benefit), typeof(BenefitDto)();
        }
    }
}

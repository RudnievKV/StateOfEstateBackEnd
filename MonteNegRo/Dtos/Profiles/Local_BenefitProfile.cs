using AutoMapper;
using MonteNegRo.Dtos.Local_BenefitDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class Local_BenefitProfile : Profile
    {
        public Local_BenefitProfile()
        {
            CreateMap<Local_Benefit, Local_BenefitDto>();
        }
    }
}

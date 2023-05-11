using AutoMapper;
using MonteNegRo.Dtos.Local_PropertyDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class Local_PropertyProfile : Profile
    {
        public Local_PropertyProfile()
        {
            CreateMap<Local_Property, Local_PropertyDto>();
        }
    }
}

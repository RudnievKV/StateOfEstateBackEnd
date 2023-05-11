using AutoMapper;
using MonteNegRo.Dtos.LocalDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class LocalProfile : Profile
    {
        public LocalProfile()
        {
            CreateMap<Local, LocalDto>();
        }
    }
}

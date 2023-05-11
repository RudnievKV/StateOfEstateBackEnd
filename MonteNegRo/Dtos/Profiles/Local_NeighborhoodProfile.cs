using AutoMapper;
using MonteNegRo.Dtos.Local_NeighborhoodDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class Local_NeighborhoodProfile : Profile
    {
        public Local_NeighborhoodProfile()
        {
            CreateMap<Local_Neighborhood, Local_NeighborhoodDto>();
        }
    }
}

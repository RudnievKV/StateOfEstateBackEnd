using AutoMapper;
using MonteNegRo.Dtos.NeighborhoodDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class NeighborhoodProfile : Profile
    {
        public NeighborhoodProfile()
        {
            CreateMap<Neighborhood, NeighborhoodDto>();
        }
    }
}

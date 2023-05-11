using AutoMapper;
using MonteNegRo.Dtos.PhotoDtos;
using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}

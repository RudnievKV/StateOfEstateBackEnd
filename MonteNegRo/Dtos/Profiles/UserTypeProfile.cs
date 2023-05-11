using AutoMapper;
using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Dtos.UserTypeDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class UserTypeProfile : Profile
    {
        public UserTypeProfile()
        {
            CreateMap<UserType, UserTypeDto>();
        }
    }
}

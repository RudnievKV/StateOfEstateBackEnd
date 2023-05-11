using AutoMapper;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PhotoDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, PhotoDto>();
        }
    }
}

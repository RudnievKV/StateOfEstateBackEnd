using AutoMapper;
using MonteNegRo.Dtos.AdvertisementSettingDtos;
using MonteNegRo.Dtos.NotificationDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDto>();
        }
    }
}

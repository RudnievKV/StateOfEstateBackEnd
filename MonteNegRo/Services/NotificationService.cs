using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonteNegRo.Common;
using MonteNegRo.DBContext;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.NotificationDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Filters;
using MonteNegRo.Models;
using MonteNegRo.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonteNegRo.Services
{
    public class NotificationService : INotificationService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        public NotificationService(MyDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<NotificationDto>> GetAllNotifications()
        {
            var notifications = await _dbContext.Notifications.ToListAsync();
            var notificationDtos = new List<NotificationDto>();
            foreach (var notification in notifications)
            {
                var notificationDto = _mapper.Map<NotificationDto>(notification);
                notificationDtos.Add(notificationDto);
            }
            return notificationDtos;
        }

        public async Task<(IEnumerable<NotificationDto> notificationDtos,
            PaginationFilter filter,
            int totalRecords)>
            GetAllNotificationsPaged(NotificationPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 40);

            var notificationsIQueryable = _dbContext.Notifications.AsQueryable();
            if (query.Search != null & query.Search != "")
            {
                notificationsIQueryable = _dbContext.Notifications
                    .Where(s => s.Notification_ID.ToString().Contains(query.Search)
                    || s.Property_ID.ToString().Contains(query.Search)
                    || s.ActivationDate.ToString().Contains(query.Search)
                    || s.Description.Contains(query.Search));
            }


            notificationsIQueryable = notificationsIQueryable
                .OrderBy(s => s.Notification_ID)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);
            var totalRecords = await notificationsIQueryable.CountAsync();

            var notifications = await notificationsIQueryable
                .ToListAsync();

            var notificationDtos = new List<NotificationDto>();
            foreach (var notification in notifications)
            {
                var notificationDto = _mapper.Map<NotificationDto>(notification);
                notificationDtos.Add(notificationDto);
            }
            return (notificationDtos, paginationFilter, totalRecords);

        }

        public async Task<NotificationDto> GetNotification(long id)
        {
            var notification = await _dbContext.Notifications.FindAsync(id);
            if (notification == null)
            {
                return null;
            }
            var notificationDto = _mapper.Map<NotificationDto>(notification);
            return notificationDto;
        }

        public async Task<NotificationDto> CreateNotification(NotificationCreateDto notificationCreateDto)
        {
            var newNotification = new Notification()
            {
                ActivationDate = notificationCreateDto.ActivationDate,
                IsActive = notificationCreateDto.IsActive,
                Description = notificationCreateDto.Description,
                Property_ID = notificationCreateDto.Property_ID,
            };
            _dbContext.Notifications.Add(newNotification);
            await _dbContext.SaveChangesAsync();
            var notificationDto = await GetNotification(newNotification.Notification_ID);
            return notificationDto;
        }

        public async Task<bool> DeleteNotification(long id)
        {
            var notification = await _dbContext.Notifications.FindAsync(id);
            if (notification == null)
            {
                return false;
            }
            _dbContext.Notifications.Remove(notification);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<NotificationDto> UpdateNotification(long id, NotificationUpdateDto notificationUpdateDto)
        {
            var notification = await _dbContext.Notifications.AsNoTracking()
                .Where(s => s.Notification_ID == id)
                .SingleOrDefaultAsync();
            if (notification == null)
            {
                return null;
            }
            var newNotification = new Notification()
            {
                ActivationDate = notificationUpdateDto.ActivationDate,
                IsActive = notificationUpdateDto.IsActive,
                Description = notificationUpdateDto.Description,
                Property_ID = notificationUpdateDto.Property_ID,
                Notification_ID = id
            };
            _dbContext.Notifications.Update(newNotification);
            await _dbContext.SaveChangesAsync();
            var notificationDto = await GetNotification(newNotification.Notification_ID);
            return notificationDto;
        }

        public async Task<bool> DeleteNotifications(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var notification = await _dbContext.Notifications.FindAsync(id);
                if (notification == null)
                {
                    return false;
                }
                _dbContext.Notifications.Remove(notification);
            }
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

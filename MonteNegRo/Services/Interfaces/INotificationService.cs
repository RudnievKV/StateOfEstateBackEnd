using MonteNegRo.Dtos.CounterpartyDtos;
using MonteNegRo.Dtos.NotificationDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface INotificationService
    {
        public Task<NotificationDto> GetNotification(long id);
        public Task<IEnumerable<NotificationDto>> GetAllNotifications();
        public Task<(IEnumerable<NotificationDto> notificationDtos, PaginationFilter filter, int totalRecords)> GetAllNotificationsPaged(NotificationPaginatedQuery query);
        public Task<NotificationDto> CreateNotification(NotificationCreateDto notificationCreateDto);
        public Task<NotificationDto> UpdateNotification(long id, NotificationUpdateDto notificationUpdateDto);
        public Task<bool> DeleteNotification(long id);
        public Task<bool> DeleteNotifications(IEnumerable<long> ids);
    }
}

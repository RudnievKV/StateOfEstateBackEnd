using MonteNegRo.Dtos.PropertyDtos;
using System;

namespace MonteNegRo.Dtos.NotificationDtos
{
    public record NotificationDto
    {
        public long Notification_ID { get; init; }
        public string? Description { get; init; }
        public DateTimeOffset ActivationDate { get; init; }
        public bool IsActive { get; init; }

        //public virtual PropertyDto Property { get; init; }
        public long Property_ID { get; init; }
    }
}

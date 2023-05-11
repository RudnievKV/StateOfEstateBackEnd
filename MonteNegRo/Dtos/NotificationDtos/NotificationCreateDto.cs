using MonteNegRo.Dtos.PropertyDtos;
using System;

namespace MonteNegRo.Dtos.NotificationDtos
{
    public record NotificationCreateDto
    {
        public string? Description { get; init; }
        public DateTimeOffset ActivationDate { get; init; }
        public bool IsActive { get; init; }
        public long Property_ID { get; init; }
    }
}

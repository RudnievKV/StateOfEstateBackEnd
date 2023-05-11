using MonteNegRo.Dtos.PhotoDtos;
using MonteNegRo.Dtos.PropertyDtos;
using System.Collections.Generic;
using static MonteNegRo.Models.Counterparty;

namespace MonteNegRo.Dtos.CounterpartyDtos
{
    public record CounterpartyDto
    {
        public long Counterparty_ID { get; init; }
        public string Type { get; init; }
        public string? FullName { get; init; }
        public string? PhoneNumber { get; init; }
        public string? PhoneNumber2 { get; init; }
        public string? PhoneNumber3 { get; init; }
        public string? Viber { get; init; }
        public string? WhatsUp { get; init; }
        public string? Telegram { get; init; }
        public string? Email { get; init; }
        public string? Website { get; init; }
        public string? Description { get; init; }
        public bool IsActive { get; init; }
        public virtual IEnumerable<PropertyIdAndPhoto>? Properties { get; init; }
    }
    public record PropertyIdAndPhoto
    {
        public long Property_ID { get; init; }
        public virtual IEnumerable<PhotoDto>? Photos { get; init; }
    }
}

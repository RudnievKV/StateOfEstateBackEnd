using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.Partner_CityDtos;
using MonteNegRo.Dtos.PartnerPhoneDtos;
using MonteNegRo.Models;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.PartnerDtos
{
    public record PartnerDto
    {
        public long Partner_ID { get; init; }
        public bool IsActiveRent { get; init; }
        public bool IsActiveSale { get; init; }
        public string? Email { get; init; }
        public string? Website { get; init; }
        public string? Notes { get; init; }
        public bool IsSubscribedRent { get; init; }
        public bool IsSubscribedSale { get; init; }
        public virtual IEnumerable<CityDto>? Cities { get; init; }
        public virtual IEnumerable<PartnerPhoneDto>? PartnerPhones { get; init; }
    }
}

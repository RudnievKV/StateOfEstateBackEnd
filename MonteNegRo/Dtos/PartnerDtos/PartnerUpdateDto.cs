using System.Collections.Generic;

namespace MonteNegRo.Dtos.PartnerDtos
{
    public class PartnerUpdateDto
    {
        public bool IsActiveRent { get; init; }
        public bool IsActiveSale { get; init; }
        public string? Email { get; init; }
        public string? Website { get; init; }
        public string? Notes { get; init; }
        public bool IsSubscribedRent { get; init; }
        public bool IsSubscribedSale { get; init; }
        public virtual IEnumerable<long>? CityIDs { get; init; }
        public virtual IEnumerable<PartnerPhoneCreateDto>? PartnerPhones { get; init; }
    }
}

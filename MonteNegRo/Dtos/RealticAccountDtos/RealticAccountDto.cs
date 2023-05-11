using MonteNegRo.Models;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.RealticAccountDtos
{
    public record RealticAccountDto
    {
        public long RealticAccount_ID { get; init; }
        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        //public virtual IEnumerable<RealticAccount_AdvertisementSettingDto>? Account_AdvertisementSettings { get; init; }

    }
}

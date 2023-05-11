using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.RealticAccountDtos;
using MonteNegRo.Models;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.AdvertisementSettingDtos
{
    public record AdvertisementSettingDto
    {
        public long AdvertisementSetting_ID { get; init; }
        public bool FacebookRent { get; init; }
        public bool InstagramRent { get; init; }
        public bool FacebookSale { get; init; }
        public bool InstagramSale { get; init; }
        public bool HomesOverseasSale { get; init; }
        //public virtual PropertyDto Property { get; init; }
        public long Property_ID { get; init; }
        public long? RealticAccountSale_ID { get; init; }
        public virtual RealticAccountDto? RealticAccountSale { get; init; }
        public long? RealticAccountRent_ID { get; init; }
        public virtual RealticAccountDto? RealticAccountRent { get; init; }
    }
}

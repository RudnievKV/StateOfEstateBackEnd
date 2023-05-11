using MonteNegRo.Dtos.PropertyDtos;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.AdvertisementSettingDtos
{
    public record AdvertisementSettingUpdateDto
    {
        public bool FacebookRent { get; init; }
        public bool InstagramRent { get; init; }
        public bool FacebookSale { get; init; }
        public bool InstagramSale { get; init; }
        public bool HomesOverseasSale { get; init; }
        public long? RealticAccountRent_ID { get; init; }
        public long? RealticAccountSale_ID { get; init; }
    }
}

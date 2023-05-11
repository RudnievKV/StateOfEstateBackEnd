using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MonteNegRo.Models
{
    public record AdvertisementSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AdvertisementSetting_ID { get; init; }
        public bool FacebookRent { get; init; }
        public bool InstagramRent { get; init; }
        public bool FacebookSale { get; init; }
        public bool InstagramSale { get; init; }
        public bool HomesOverseasSale { get; init; }
        public virtual Property Property { get; init; }
        public long Property_ID { get; init; }



        public long? RealticAccountSale_ID { get; init; }
        public virtual RealticAccount? RealticAccountSale { get; init; }

        public long? RealticAccountRent_ID { get; init; }
        public virtual RealticAccount? RealticAccountRent { get; init; }

        //public virtual IEnumerable<RealticAccount_AdvertisementSetting>? RealticAccount_AdvertisementSettings { get; init; }
    }
}

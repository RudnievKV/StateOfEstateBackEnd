using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MonteNegRo.Models
{
    public record RealticAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RealticAccount_ID { get; init; }
        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public virtual IEnumerable<AdvertisementSetting>? AdvertisementSettingsSale { get; init; }
        public virtual IEnumerable<AdvertisementSetting>? AdvertisementSettingsRent { get; init; }
        //public virtual IEnumerable<RealticAccount_AdvertisementSetting>? RealticAccount_AdvertisementSettings { get; init; }
    }
}

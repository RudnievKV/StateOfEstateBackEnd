using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MonteNegRo.Models
{
    public record Partner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Partner_ID { get; init; }
        public bool IsActiveRent { get; init; }
        public bool IsActiveSale { get; init; }
        public string? Email { get; init; }
        public string? Website { get; init; }
        public string? Notes { get; init; }
        public bool IsSubscribedRent { get; init; }
        public bool IsSubscribedSale { get; init; }
        public virtual IEnumerable<Partner_City>? Partner_Cities { get; init; }
        public virtual IEnumerable<PartnerPhone>? PartnerPhones { get; init; }
    }
}

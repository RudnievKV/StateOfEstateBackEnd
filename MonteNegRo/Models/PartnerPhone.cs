using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MonteNegRo.Models
{
    public record PartnerPhone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PartnerPhone_ID { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Note { get; init; }
        public long Partner_ID { get; init; }
        public Partner Partner { get; init; }

    }
}

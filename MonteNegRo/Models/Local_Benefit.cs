using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MonteNegRo.Models
{
    public record Local_Benefit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Local_Benefit_ID { get; init; }
        public string? LocalBenefitName { get; init; }


        public long Local_ID { get; init; }
        public virtual Local Local { get; init; }
        public long Benefit_ID { get; init; }
        public virtual Benefit Benefit { get; init; }
    }
}

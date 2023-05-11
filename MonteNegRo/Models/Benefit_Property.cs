using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonteNegRo.Models
{
    public record Benefit_Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Benefit_Property_ID { get; init; }
        public long Benefit_ID { get; init; }
        public long Property_ID { get; init; }
        public virtual Benefit Benefit { get; init; }
        public virtual Property Property { get; init; }
    }
}

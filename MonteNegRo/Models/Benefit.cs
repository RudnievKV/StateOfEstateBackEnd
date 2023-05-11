using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonteNegRo.Models
{
    public record Benefit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Benefit_ID { get; init; }
        public virtual IEnumerable<Benefit_Property>? Benefit_Properties { get; init; }
        public virtual IEnumerable<Local_Benefit>? Local_Benefits { get; init; }
    }
}

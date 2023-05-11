using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MonteNegRo.Models
{
    public record Neighborhood
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Neighborhood_ID { get; init; }
        public long City_ID { get; init; }
        public virtual City City { get; init; }
        public virtual IEnumerable<Local_Neighborhood>? Local_Neighborhoods { get; init; }
    }
}

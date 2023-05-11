using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace MonteNegRo.Models
{
    public record Local
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Local_ID { get; init; }
        public string LocalizationCode { get; init; }

        public virtual IEnumerable<Local_Benefit>? Local_Benefits { get; init; }
        public virtual IEnumerable<Local_City>? Local_Cities { get; init; }
        public virtual IEnumerable<Local_Property>? Local_Properties { get; init; }
        public virtual IEnumerable<Local_Neighborhood>? Local_Neighborhoods { get; init; }

    }
}

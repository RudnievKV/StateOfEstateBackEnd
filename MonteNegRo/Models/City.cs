using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonteNegRo.Models
{
    public record City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long City_ID { get; init; }
        public virtual IEnumerable<City_Property>? City_Properties { get; init; }
        public virtual IEnumerable<Local_City>? Local_Cities { get; init; }
        public virtual IEnumerable<Neighborhood>? Neighborhoods { get; init; }
        public virtual IEnumerable<Partner_City>? Partner_Cities { get; init; }
    }
}

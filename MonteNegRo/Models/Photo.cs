using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonteNegRo.Models
{
    public record Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Photo_ID { get; init; }
        public string PhotoUrl { get; init; }
        public string PhotoName { get; init; }
        public virtual IEnumerable<Photo_Property> Photo_Properties { get; init; }
    }
}

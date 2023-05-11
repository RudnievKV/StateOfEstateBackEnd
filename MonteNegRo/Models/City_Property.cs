using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonteNegRo.Models
{
    public record City_Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long City_Property_ID { get; init; }
        public long City_ID { get; init; }
        public long Property_ID { get; init; }
        public virtual City City { get; init; }
        public virtual Property Property { get; init; }
    }
}

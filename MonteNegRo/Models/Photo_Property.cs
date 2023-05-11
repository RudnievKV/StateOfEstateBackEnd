using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonteNegRo.Models
{
    public record Photo_Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Photo_Property_ID { get; init; }
        public long Property_ID { get; init; }
        public long Photo_ID { get; init; }
        public int Position { get; init; }
        public virtual Photo Photo { get; init; }
        public virtual Property Property { get; init; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MonteNegRo.Models
{
    public record Partner_City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Partner_City_ID { get; init; }
        public long Partner_ID { get; init; }
        public long City_ID { get; init; }
        public virtual City City { get; init; }
        public virtual Partner Partner { get; init; }
    }
}

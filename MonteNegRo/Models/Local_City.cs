using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MonteNegRo.Models
{
    public record Local_City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Local_City_ID { get; init; }
        public string? LocalCityName { get; init; }

        public long Local_ID { get; init; }
        public virtual Local Local { get; init; }
        public long City_ID { get; init; }
        public virtual City City { get; init; }
    }
}

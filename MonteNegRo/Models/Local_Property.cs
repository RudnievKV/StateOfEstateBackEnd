using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MonteNegRo.Models
{
    public record Local_Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Local_Property_ID { get; init; }
        public string? LocalPropertyTitle { get; init; }
        public string? LocalPropertyDescription { get; init; }
        public LocalPropertyStatus Type { get; init; }
        public enum LocalPropertyStatus
        {
            Rent,
            Sale
        }
        public long Property_ID { get; init; }
        public virtual Property Property { get; init; }
        public long Local_ID { get; init; }
        public virtual Local Local { get; init; }

    }
}

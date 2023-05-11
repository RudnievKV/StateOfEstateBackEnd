using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MonteNegRo.Models
{
    public record Local_Neighborhood
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Local_Neighborhood_ID { get; init; }
        public string LocalNeighborhoodName { get; init; }


        public long Local_ID { get; init; }
        public virtual Local Local { get; init; }
        public long Neighborhood_ID { get; init; }
        public virtual Neighborhood? Neighborhood { get; init; }
    }
}

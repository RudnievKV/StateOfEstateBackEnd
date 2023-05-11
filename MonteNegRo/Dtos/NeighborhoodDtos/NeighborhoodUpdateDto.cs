using MonteNegRo.Dtos.Local_NeighborhoodDtos;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.NeighborhoodDtos
{
    public record NeighborhoodUpdateDto
    {
        public long City_ID { get; init; }
        public virtual IEnumerable<LocalNeighborhoodValue> Local_Neighborhoods { get; init; }
    }
}

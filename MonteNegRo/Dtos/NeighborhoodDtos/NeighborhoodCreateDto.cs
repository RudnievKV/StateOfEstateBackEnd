using MonteNegRo.Dtos.Local_NeighborhoodDtos;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.NeighborhoodDtos
{
    public record NeighborhoodCreateDto
    {
        public long City_ID { get; init; }
        public virtual IEnumerable<LocalNeighborhoodValue> Local_Neighborhoods { get; init; }
    }
    public record LocalNeighborhoodValue
    {
        public string? LocalNeighborhoodName { get; init; }
        public long Local_ID { get; init; }
    }
}

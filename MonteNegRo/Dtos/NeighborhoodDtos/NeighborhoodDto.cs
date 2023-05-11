using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.Local_NeighborhoodDtos;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.NeighborhoodDtos
{
    public record NeighborhoodDto
    {
        public long Neighborhood_ID { get; init; }
        public long City_ID { get; init; }

        //public CityDto CityDto { get; init; }
        public virtual IEnumerable<Local_NeighborhoodDto>? Local_Neighborhoods { get; init; }
    }
}

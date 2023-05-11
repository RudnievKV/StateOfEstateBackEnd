using MonteNegRo.Dtos.Local_CityDtos;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.CityDtos
{
    public record CityUpdateDto
    {
        public virtual IEnumerable<LocalCityValue> Local_Cities { get; init; }
    }
}

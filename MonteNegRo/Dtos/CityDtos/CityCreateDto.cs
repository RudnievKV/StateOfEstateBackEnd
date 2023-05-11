using MonteNegRo.Dtos.Local_CityDtos;
using MonteNegRo.Models;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.CityDtos
{
    public record CityCreateDto
    {
        public virtual IEnumerable<LocalCityValue> Local_Cities { get; init; }
    }
    public record LocalCityValue
    {
        public string? LocalCityName { get; init; }
        public long Local_ID { get; init; }
    }
}

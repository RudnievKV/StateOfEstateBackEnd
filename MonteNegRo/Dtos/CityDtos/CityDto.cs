using MonteNegRo.Dtos.City_PropertyDtos;
using MonteNegRo.Dtos.Local_CityDtos;
using MonteNegRo.Dtos.NeighborhoodDtos;
using MonteNegRo.Models;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.CityDtos
{
    public record CityDto
    {
        public long City_ID { get; init; }
        public virtual IEnumerable<Local_CityDto> Local_Cities { get; init; }
        public virtual IEnumerable<NeighborhoodDto>? Neighborhoods { get; init; }
        //public virtual IEnumerable<City_PropertyDto> City_Properties { get; init; }
    }
}

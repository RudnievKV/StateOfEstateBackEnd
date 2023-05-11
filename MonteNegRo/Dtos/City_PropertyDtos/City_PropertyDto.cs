using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.City_PropertyDtos
{
    public record City_PropertyDto
    {
        public long City_Property_ID { get; init; }
        public long City_ID { get; init; }
        public long Property_ID { get; init; }
        public virtual CityDto City { get; init; }
        //public virtual PropertyDto Property { get; init; }
    }
}

using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PartnerDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Partner_CityDtos
{
    public record Partner_CityDto
    {
        public long Partner_City_ID { get; init; }
        public long Partner_ID { get; init; }
        public long City_ID { get; init; }
        public virtual CityDto City { get; init; }
        public virtual PartnerDto Partner { get; init; }
    }
}

using MonteNegRo.Dtos.LocalDtos;

namespace MonteNegRo.Dtos.Local_CityDtos
{
    public record Local_CityDto
    {
        public long Local_City_ID { get; init; }
        public virtual LocalDto Local { get; init; }
        public string? LocalCityName { get; init; }
    }
}

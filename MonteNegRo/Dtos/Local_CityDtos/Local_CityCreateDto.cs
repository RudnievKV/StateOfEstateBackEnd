using MonteNegRo.Dtos.LocalDtos;

namespace MonteNegRo.Dtos.Local_CityDtos
{
    public record Local_CityCreateDto
    {
        public string? LocalCityName { get; init; }
        public long City_ID { get; init; }
        public long Local_ID { get; init; }
    }
}

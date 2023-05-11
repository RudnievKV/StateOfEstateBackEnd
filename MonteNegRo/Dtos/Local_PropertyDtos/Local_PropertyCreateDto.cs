using MonteNegRo.Dtos.LocalDtos;

namespace MonteNegRo.Dtos.Local_PropertyDtos
{
    public record Local_PropertyCreateDto
    {
        public long Property_ID { get; init; }
        public string? LocalPropertyTitle { get; init; }
        public string? LocalPropertyDescription { get; init; }
        public long Local_ID { get; init; }
    }
}

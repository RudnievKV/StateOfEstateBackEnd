using MonteNegRo.Dtos.LocalDtos;

namespace MonteNegRo.Dtos.Local_PropertyDtos
{
    public record Local_PropertyDto
    {
        public long Local_Property_ID { get; init; }
        public virtual LocalDto Local { get; init; }
        public string? LocalPropertyTitle { get; init; }
        public string? LocalPropertyDescription { get; init; }
    }
}

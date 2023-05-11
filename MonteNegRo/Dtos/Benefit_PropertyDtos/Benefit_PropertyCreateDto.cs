using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Benefit_PropertyDtos
{
    public record Benefit_PropertyCreateDto
    {
        public long Benefit_ID { get; init; }
        public long Property_ID { get; init; }
    }
}

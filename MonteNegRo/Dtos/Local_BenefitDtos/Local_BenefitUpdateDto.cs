using MonteNegRo.Dtos.LocalDtos;

namespace MonteNegRo.Dtos.Local_BenefitDtos
{
    public record Local_BenefitUpdateDto
    {
        public string? LocalBenefitName { get; init; }
        public long Benefit_ID { get; init; }
        public long Local_ID { get; init; }
    }
}

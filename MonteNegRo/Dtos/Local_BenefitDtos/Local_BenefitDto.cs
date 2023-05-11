using MonteNegRo.Dtos.LocalDtos;

namespace MonteNegRo.Dtos.Local_BenefitDtos
{
    public record Local_BenefitDto
    {
        public long Local_Benefit_ID { get; init; }
        public virtual LocalDto Local { get; init; }
        public string? LocalBenefitName { get; init; }
    }
}

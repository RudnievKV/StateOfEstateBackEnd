using MonteNegRo.Dtos.LocalDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Local_BenefitDtos
{
    public record Local_BenefitCreateDto
    {
        public string? LocalBenefitName { get; init; }
        public long Benefit_ID { get; init; }
        public long Local_ID { get; init; }
    }
}

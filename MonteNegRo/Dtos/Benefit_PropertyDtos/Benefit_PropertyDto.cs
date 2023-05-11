using MonteNegRo.Dtos.BenefitDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Models;

namespace MonteNegRo.Dtos.Benefit_PropertyDtos
{
    public record Benefit_PropertyDto
    {
        public long Benefit_Property_ID { get; init; }
        public long Benefit_ID { get; init; }
        public long Property_ID { get; init; }
        public virtual BenefitDto Benefit { get; init; }
        //public virtual PropertyDto Property { get; init; }
    }
}

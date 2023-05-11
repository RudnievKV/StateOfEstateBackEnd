using MonteNegRo.Dtos.Local_BenefitDtos;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.BenefitDtos
{
    public record BenefitUpdateDto
    {
        public virtual IEnumerable<LocalBenefitValue> Local_Benefits { get; init; }
    }
}

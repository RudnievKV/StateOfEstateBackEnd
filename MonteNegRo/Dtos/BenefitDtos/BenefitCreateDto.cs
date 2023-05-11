using MonteNegRo.Dtos.Local_BenefitDtos;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MonteNegRo.Dtos.BenefitDtos
{
    public record BenefitCreateDto
    {
        public virtual IEnumerable<LocalBenefitValue> Local_Benefits { get; init; }
    }
    public record LocalBenefitValue
    {
        public string? LocalBenefitName { get; init; }
        public long Local_ID { get; init; }
    }
}

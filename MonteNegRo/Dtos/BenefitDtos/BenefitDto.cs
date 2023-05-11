using MonteNegRo.Dtos.Benefit_PropertyDtos;
using MonteNegRo.Dtos.Local_BenefitDtos;
using MonteNegRo.Models;
using System.Collections.Generic;

namespace MonteNegRo.Dtos.BenefitDtos
{
    public record BenefitDto
    {
        public long Benefit_ID { get; init; }
        public virtual IEnumerable<Local_BenefitDto> Local_Benefits { get; init; }
        //public virtual IEnumerable<Benefit_PropertyDto> Benefit_Properties { get; init; }
    }
}

using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Dtos.BenefitDtos;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Filters;
using MonteNegRo.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface IBenefitService
    {
        public Task<BenefitDto> GetBenefit(long id);
        public Task<IEnumerable<BenefitDto>> GetAllBenefits();
        public Task<(IEnumerable<BenefitDto> benefitDtos, PaginationFilter filter, int totalRecords)> GetAllBenefitsPaged(BenefitPaginatedQuery query);
        public Task<BenefitDto> CreateBenefit(BenefitCreateDto benefitCreateDto);
        public Task<BenefitDto> UpdateBenefit(long id, BenefitUpdateDto benefitUpdateDto);
        public Task<bool> DeleteBenefit(long id);
        public Task<bool> DeleteBenefits(IEnumerable<long> ids);
    }
}

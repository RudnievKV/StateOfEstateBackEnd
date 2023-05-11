using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PartnerDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface IPartnerService
    {
        public Task<PartnerDto> GetPartner(long id);
        public Task<IEnumerable<PartnerDto>> GetAllPartners();
        public Task<(IEnumerable<PartnerDto> partnerDtos, PaginationFilter filter, int totalRecords)> GetAllPartnersPaged(PartnerPaginatedQuery query);
        public Task<PartnerDto> CreatePartner(PartnerCreateDto partnerCreateDto);
        public Task<PartnerDto> UpdatePartner(long id, PartnerUpdateDto partnerUpdateDto);
        public Task<bool> DeletePartner(long id);
        public Task<bool> DeletePartners(IEnumerable<long> ids);
    }
}

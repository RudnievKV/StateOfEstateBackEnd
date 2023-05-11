using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Dtos.PartnerDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Dtos.RealticAccountDtos;
using MonteNegRo.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface IRealticAccountService
    {
        public Task<RealticAccountDto> GetRealticAccount(long id);
        public Task<IEnumerable<RealticAccountDto>> GetAllRealticAccounts();
        public Task<(IEnumerable<RealticAccountDto> realticAccountDtos, PaginationFilter filter, int totalRecords)> GetAllRealticAccountsPaged(RealticAccountPaginatedQuery query);
        public Task<RealticAccountDto> CreateRealticAccount(RealticAccountCreateDto realticAccountCreateDto);
        public Task<RealticAccountDto> UpdateRealticAccount(long id, RealticAccountUpdateDto realticAccountUpdateDto);
        public Task<bool> DeleteRealticAccount(long id);
    }
}

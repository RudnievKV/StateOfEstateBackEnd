using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Dtos.LocalDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Dtos.UserTypeDtos;
using MonteNegRo.Filters;
using MonteNegRo.Models;
using MonteNegRo.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface ILocalService
    {
        public Task<LocalDto> GetLocal(long id);
        public Task<(IEnumerable<LocalDto> localDtos, PaginationFilter filter, int totalRecords)> GetAllLocals(LocalPaginatedQuery query);
        public Task<LocalDto> CreateLocal(LocalCreateDto localCreateDto);
        public Task<LocalDto> UpdateLocal(long id, LocalUpdateDto localUpdateDto);
        public Task<bool> DeleteLocal(long id);

    }
}

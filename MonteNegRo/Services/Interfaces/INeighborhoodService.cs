using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Dtos.LocalDtos;
using MonteNegRo.Dtos.NeighborhoodDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Filters;
using MonteNegRo.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface INeighborhoodService
    {
        public Task<NeighborhoodDto> GetNeighborhood(long id);
        public Task<(IEnumerable<NeighborhoodDto> neighborhoodDtos, PaginationFilter filter, int totalRecords)> GetAllNeighborhoodsPaged(NeighborhoodPaginatedQuery query);
        public Task<IEnumerable<NeighborhoodDto>> GetAllNeighborhoods();
        public Task<NeighborhoodDto> CreateNeighborhood(NeighborhoodCreateDto neighborhoodCreateDto);
        public Task<NeighborhoodDto> UpdateNeighborhood(long id, NeighborhoodUpdateDto neighborhoodUpdateDto);
        public Task<bool> DeleteNeighborhood(long id);
        public Task<bool> DeleteNeighborhoods(IEnumerable<long> ids);

    }
}

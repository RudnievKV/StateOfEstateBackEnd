using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Filters;
using MonteNegRo.Models;
using MonteNegRo.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface ICityService
    {
        public Task<CityDto> GetCity(long id);
        public Task<IEnumerable<CityDto>> GetCities(IEnumerable<long> ids);
        public Task<IEnumerable<CityDto>> GetAllCities();
        public Task<(IEnumerable<CityDto> cityDtos, PaginationFilter filter, int totalRecords)> GetAllCitiesPaged(CityPaginatedQuery query);
        public Task<CityDto> CreateCity(CityCreateDto cityCreateDto);
        public Task<CityDto> UpdateCity(long id, CityUpdateDto cityUpdateDto);
        public Task<bool> DeleteCity(long id);
        public Task<bool> DeleteCities(IEnumerable<long> ids);
    }
}

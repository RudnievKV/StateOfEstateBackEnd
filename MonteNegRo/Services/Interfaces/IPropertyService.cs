using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Filters;
using MonteNegRo.Models;
using MonteNegRo.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface IPropertyService
    {
        public Task<PropertyDto> GetProperty(long id);
        public Task<IEnumerable<PropertyDto>> GetAllProperties();
        public Task<IEnumerable<PropertyDto>> GetSimilarProperties(long id);
        public Task<(IEnumerable<PropertyDto> propertyDtos, PaginationFilter filter, int totalRecords)> GetAllPropertiesPaged(PropertyPaginatedQuery query);
        public Task<PropertyDto> CreateProperty(PropertyCreateDto propertyCreateDto);
        public Task<PropertyDto> UpdateProperty(long id, PropertyUpdateDto propertyUpdateDto);
        public Task<bool> DeleteProperty(long id);
        public Task<bool> DeleteProperties(IEnumerable<long> ids);
        public Task<(IEnumerable<PropertyDto> propertyDtos, PaginationFilter filter, int totalRecords)> SearchProperties(PropertySearchPaginatedQuery query);
    }
}

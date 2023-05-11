using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.CounterpartyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface ICounterpartyService
    {
        public Task<CounterpartyDto> GetCounterparty(long id);
        public Task<IEnumerable<CounterpartyDto>> GetAllCounterparties();
        public Task<(IEnumerable<CounterpartyDto> counterpartyDtos, PaginationFilter filter, int totalRecords)> GetAllCounterpartiesPaged(CounterpartyPaginatedQuery query);
        public Task<CounterpartyDto> CreateCounterparty(CounterpartyCreateDto counterpartyCreateDto);
        public Task<CounterpartyDto> UpdateCounterparty(long id, CounterpartyUpdateDto counterpartyUpdateDto);
        public Task<bool> DeleteCounterparty(long id);
    }
}

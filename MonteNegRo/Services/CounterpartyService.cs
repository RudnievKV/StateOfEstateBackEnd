using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonteNegRo.Common;
using MonteNegRo.DBContext;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.CounterpartyDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Filters;
using MonteNegRo.Models;
using MonteNegRo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MonteNegRo.Models.Counterparty;
using static MonteNegRo.Models.Property;

namespace MonteNegRo.Services
{
    public class CounterpartyService : ICounterpartyService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        public CounterpartyService(MyDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CounterpartyDto>> GetAllCounterparties()
        {
            var counterparties = await _dbContext.Counterparties.ToListAsync();

            var counterpartyDtos = new List<CounterpartyDto>();
            foreach (var counterparty in counterparties)
            {
                var cityDto = _mapper.Map<CounterpartyDto>(counterparty);
                counterpartyDtos.Add(cityDto);
            }
            return counterpartyDtos;
        }

        public async Task<(IEnumerable<CounterpartyDto> counterpartyDtos,
            PaginationFilter filter,
            int totalRecords)>
            GetAllCounterpartiesPaged(CounterpartyPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 20);

            var counterpartiesIQueryable = _dbContext.Counterparties
                .OrderBy(s => s.Counterparty_ID)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);

            var totalRecords = await counterpartiesIQueryable.CountAsync();
            var counterparties = await counterpartiesIQueryable
                .ToListAsync();

            var counterpartyDtos = new List<CounterpartyDto>();
            foreach (var counterparty in counterparties)
            {
                var counterpartyDto = _mapper.Map<CounterpartyDto>(counterparty);
                counterpartyDtos.Add(counterpartyDto);
            }
            return (counterpartyDtos, paginationFilter, totalRecords);
        }

        public async Task<CounterpartyDto> GetCounterparty(long id)
        {
            var counterparty = await _dbContext.Counterparties.FindAsync(id);
            if (counterparty == null)
            {
                return null;
            }
            var counterpartyDto = _mapper.Map<CounterpartyDto>(counterparty);
            return counterpartyDto;
        }

        public async Task<CounterpartyDto> CreateCounterparty(CounterpartyCreateDto counterpartyCreateDto)
        {
            var newCounterparty = new Counterparty()
            {
                IsActive = counterpartyCreateDto.IsActive,
                Description = counterpartyCreateDto.Description,
                Email = counterpartyCreateDto.Email,
                FullName = counterpartyCreateDto.FullName,
                PhoneNumber = counterpartyCreateDto.PhoneNumber,
                PhoneNumber2 = counterpartyCreateDto.PhoneNumber2,
                PhoneNumber3 = counterpartyCreateDto.PhoneNumber3,
                Telegram = counterpartyCreateDto.Telegram,
                Type = Enum.Parse<CounterpartyType>(counterpartyCreateDto.Type),
                Viber = counterpartyCreateDto.Viber,
                Website = counterpartyCreateDto.Website,
                WhatsUp = counterpartyCreateDto.WhatsUp,
            };
            _dbContext.Counterparties.Add(newCounterparty);
            await _dbContext.SaveChangesAsync();
            var counterpartyDto = await GetCounterparty(newCounterparty.Counterparty_ID);
            return counterpartyDto;
        }

        public async Task<bool> DeleteCounterparty(long id)
        {
            var counterparty = await _dbContext.Counterparties.FindAsync(id);
            if (counterparty == null)
            {
                return false;
            }
            _dbContext.Counterparties.Remove(counterparty);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CounterpartyDto> UpdateCounterparty(long id, CounterpartyUpdateDto counterpartyUpdateDto)
        {
            var counterparty = await _dbContext.Counterparties.AsNoTracking()
                .Where(s => s.Counterparty_ID == id)
                .SingleOrDefaultAsync();

            if (counterparty == null)
            {
                return null;
            }
            var newCounterparty = new Counterparty()
            {
                Counterparty_ID = id,
                IsActive = counterpartyUpdateDto.IsActive,
                Description = counterpartyUpdateDto.Description,
                Email = counterpartyUpdateDto.Email,
                FullName = counterpartyUpdateDto.FullName,
                PhoneNumber = counterpartyUpdateDto.PhoneNumber,
                PhoneNumber2 = counterpartyUpdateDto.PhoneNumber2,
                PhoneNumber3 = counterpartyUpdateDto.PhoneNumber3,
                Telegram = counterpartyUpdateDto.Telegram,
                Type = Enum.Parse<CounterpartyType>(counterpartyUpdateDto.Type),
                Viber = counterpartyUpdateDto.Viber,
                Website = counterpartyUpdateDto.Website,
                WhatsUp = counterpartyUpdateDto.WhatsUp,
            };
            _dbContext.Counterparties.Update(newCounterparty);
            await _dbContext.SaveChangesAsync();
            var counterpartyDto = await GetCounterparty(newCounterparty.Counterparty_ID);
            return counterpartyDto;
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonteNegRo.Common;
using MonteNegRo.DBContext;
using MonteNegRo.Dtos.LocalDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Filters;
using MonteNegRo.Models;
using MonteNegRo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MonteNegRo.Services
{
    public class LocalService : ILocalService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        public LocalService(MyDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<LocalDto> localDtos,
            PaginationFilter filter,
            int totalRecords)>
            GetAllLocals(LocalPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 40);
            var localsIQueryable = _dbContext.Locals
                .OrderBy(s => s.Local_ID)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);
            var totalRecords = await localsIQueryable.CountAsync();
            var locals = await localsIQueryable.ToListAsync();

            var localDtos = new List<LocalDto>();
            foreach (var local in locals)
            {
                var localDto = _mapper.Map<LocalDto>(local);
                localDtos.Add(localDto);
            }
            return (localDtos, paginationFilter, totalRecords);
        }

        public async Task<LocalDto> GetLocal(long id)
        {
            var local = await _dbContext.Locals.FindAsync(id);
            var localDto = _mapper.Map<LocalDto>(local);
            return localDto;
        }

        public async Task<LocalDto> CreateLocal(LocalCreateDto localCreateDto)
        {
            var newLocal = new Local()
            {
                LocalizationCode = localCreateDto.LocalizationCode,
            };
            _dbContext.Locals.Add(newLocal);
            await _dbContext.SaveChangesAsync();
            var localDto = _mapper.Map<LocalDto>(newLocal);
            return localDto;
        }

        public async Task<bool> DeleteLocal(long id)
        {
            var local = await _dbContext.Locals.FindAsync(id);
            if (local == null)
            {
                return false;
            }
            _dbContext.Locals.Remove(local);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<LocalDto> UpdateLocal(long id, LocalUpdateDto localUpdateDto)
        {
            var newLocal = new Local()
            {
                Local_ID = id,
                LocalizationCode = localUpdateDto.LocalizationCode,
            };
            _dbContext.Locals.Update(newLocal);
            await _dbContext.SaveChangesAsync();

            var localDto = _mapper.Map<LocalDto>(newLocal);
            return localDto;
        }
    }
}

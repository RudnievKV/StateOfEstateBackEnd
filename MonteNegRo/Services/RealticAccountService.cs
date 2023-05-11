using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonteNegRo.Common;
using MonteNegRo.DBContext;
using MonteNegRo.Dtos.PartnerDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Dtos.RealticAccountDtos;
using MonteNegRo.Filters;
using MonteNegRo.Models;
using MonteNegRo.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonteNegRo.Services
{
    public class RealticAccountService : IRealticAccountService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        public RealticAccountService(MyDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RealticAccountDto>> GetAllRealticAccounts()
        {
            var realticAccounts = await _dbContext.RealticAccounts.ToListAsync();
            var realticAccountDtos = new List<RealticAccountDto>();
            foreach (var realticAccount in realticAccounts)
            {
                var realticAccountDto = _mapper.Map<RealticAccountDto>(realticAccount);
                realticAccountDtos.Add(realticAccountDto);
            }
            return realticAccountDtos;
        }

        public async Task<(IEnumerable<RealticAccountDto> realticAccountDtos,
            PaginationFilter filter,
            int totalRecords)>
            GetAllRealticAccountsPaged(RealticAccountPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 20);
            var realticAccountsIQueryable = _dbContext.RealticAccounts
                .OrderBy(s => s.RealticAccount_ID)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);
            var totalRecords = await realticAccountsIQueryable.CountAsync();

            var realticAccounts = await realticAccountsIQueryable
                .ToListAsync();

            var realticAccountDtos = new List<RealticAccountDto>();
            foreach (var realticAccount in realticAccounts)
            {
                var realticAccountDto = _mapper.Map<RealticAccountDto>(realticAccount);
                realticAccountDtos.Add(realticAccountDto);
            }
            return (realticAccountDtos, paginationFilter, totalRecords);
        }

        public async Task<RealticAccountDto> GetRealticAccount(long id)
        {
            var realticAccount = await _dbContext.RealticAccounts
                .Where(s => s.RealticAccount_ID == id)
                .SingleOrDefaultAsync();
            if (realticAccount == null)
            {
                return null;
            }
            var realticAccountDto = _mapper.Map<RealticAccountDto>(realticAccount);
            return realticAccountDto;
        }

        public async Task<RealticAccountDto> CreateRealticAccount(RealticAccountCreateDto realticAccountCreateDto)
        {
            var newRealticAccount = new RealticAccount()
            {
                Email = realticAccountCreateDto.Email,
                Name = realticAccountCreateDto.Name,
                PhoneNumber = realticAccountCreateDto.PhoneNumber,
            };
            _dbContext.RealticAccounts.Add(newRealticAccount);
            await _dbContext.SaveChangesAsync();
            var realticAccountDto = await GetRealticAccount(newRealticAccount.RealticAccount_ID);
            return realticAccountDto;
        }

        public async Task<bool> DeleteRealticAccount(long id)
        {
            var realticAccount = await _dbContext.RealticAccounts
                .Where(s => s.RealticAccount_ID == id)
                .SingleOrDefaultAsync();
            if (realticAccount != null)
            {
                return false;
            }
            _dbContext.RealticAccounts.Remove(realticAccount);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<RealticAccountDto> UpdateRealticAccount(long id, RealticAccountUpdateDto realticAccountUpdateDto)
        {
            var realticAccount = await _dbContext.RealticAccounts
                .AsNoTracking()
                .Where(s => s.RealticAccount_ID == id)
                .SingleOrDefaultAsync();
            if (realticAccount == null)
            {
                return null;
            }
            var newRealticAccount = new RealticAccount()
            {
                RealticAccount_ID = id,
                Email = realticAccountUpdateDto.Email,
                Name = realticAccountUpdateDto.Name,
                PhoneNumber = realticAccountUpdateDto.PhoneNumber,
            };
            _dbContext.RealticAccounts.Update(newRealticAccount);
            await _dbContext.SaveChangesAsync();
            var realticAccountDto = await GetRealticAccount(id);
            return realticAccountDto;
        }
    }
}

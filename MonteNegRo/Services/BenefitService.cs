using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonteNegRo.Common;
using MonteNegRo.DBContext;
using MonteNegRo.Dtos.BenefitDtos;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Filters;
using MonteNegRo.Models;
using MonteNegRo.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MonteNegRo.Services
{
    public class BenefitService : IBenefitService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        public BenefitService(IMapper mapper, MyDBContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BenefitDto>> GetAllBenefits()
        {
            var benefits = await _dbContext.Benefits
                .Include(s => s.Local_Benefits)
                .ThenInclude(s => s.Local)
                .ToListAsync();

            var benefitDtos = new List<BenefitDto>();

            foreach (var benefit in benefits)
            {
                var benefitDto = _mapper.Map<BenefitDto>(benefit);
                benefitDtos.Add(benefitDto);
            }
            return benefitDtos;
        }
        public async Task<(IEnumerable<BenefitDto> benefitDtos,
            PaginationFilter filter,
            int totalRecords)>
            GetAllBenefitsPaged(BenefitPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 20);

            var mainPredicate = PredicateBuilder.True<Benefit>();

            var benefitIQueryable = _dbContext.Benefits.AsQueryable();

            if (query.Search != null & query.Search != "")
            {
                var local_Benefits = await _dbContext.Local_Benefits
                    .Where(s => s.LocalBenefitName
                    .Contains(query.Search))
                    .ToListAsync();

                var secondaryPredicate = PredicateBuilder.False<Benefit>();
                foreach (var local_Benefit in local_Benefits)
                {
                    secondaryPredicate = secondaryPredicate.OR(s => s.Benefit_ID == local_Benefit.Benefit_ID);

                }
                mainPredicate = mainPredicate.AND(secondaryPredicate);

            }



            benefitIQueryable = benefitIQueryable
                .Where(mainPredicate)
                .OrderBy(s => s.Benefit_ID)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);
            var totalRecords = await benefitIQueryable.CountAsync();
            var benefits = await benefitIQueryable
                .Include(s => s.Local_Benefits)
                .ThenInclude(s => s.Local)
                .ToListAsync();

            var benefitDtos = new List<BenefitDto>();
            foreach (var benefit in benefits)
            {
                var benefitDto = _mapper.Map<BenefitDto>(benefit);
                benefitDtos.Add(benefitDto);
            }
            return (benefitDtos, paginationFilter, totalRecords);
        }
        public async Task<BenefitDto> GetBenefit(long id)
        {
            var benefit = await _dbContext.Benefits
                .Include(s => s.Local_Benefits)
                .ThenInclude(s => s.Local)
                .Where(s => s.Benefit_ID == id)
                .SingleOrDefaultAsync();

            if (benefit == null)
            {
                return null;
            }
            var benefitDto = _mapper.Map<BenefitDto>(benefit);
            return benefitDto;
        }
        public async Task<BenefitDto> CreateBenefit(BenefitCreateDto benefitCreateDto)
        {
            var newBenefit = new Benefit()
            {

            };
            _dbContext.Benefits.Add(newBenefit);

            foreach (var localBenefitValue in benefitCreateDto.Local_Benefits)
            {
                var newLocal_Benefit = new Local_Benefit()
                {
                    LocalBenefitName = localBenefitValue.LocalBenefitName,
                    Benefit = newBenefit,
                    Local_ID = localBenefitValue.Local_ID
                };
                _dbContext.Local_Benefits.Add(newLocal_Benefit);
            }

            await _dbContext.SaveChangesAsync();

            var benefitDto = await GetBenefit(newBenefit.Benefit_ID);

            return benefitDto;
        }
        public async Task<bool> DeleteBenefit(long id)
        {
            var benefit = await _dbContext.Benefits.FindAsync(id);

            if (benefit == null)
            {
                return false;
            }

            _dbContext.Benefits.Remove(benefit);
            await _dbContext.SaveChangesAsync();

            return true;

        }
        public async Task<BenefitDto> UpdateBenefit(long id, BenefitUpdateDto benefitUpdateDto)
        {
            var benefit = await _dbContext.Benefits.AsNoTracking()
                .Where(s => s.Benefit_ID == id)
                .SingleOrDefaultAsync();

            if (benefit == null)
            {
                return null;
            }

            var newBenefit = new Benefit()
            {
                Benefit_ID = id,
            };

            _dbContext.Benefits.Update(newBenefit);

            var local_Benefits = await _dbContext.Local_Benefits
                .Where(s => s.Benefit_ID == id)
                .ToListAsync();

            foreach (var local_Benefit in local_Benefits)
            {
                _dbContext.Local_Benefits.Remove(local_Benefit);
            }


            foreach (var localBenefitValue in benefitUpdateDto.Local_Benefits)
            {
                var newLocal_Benefit = new Local_Benefit()
                {
                    LocalBenefitName = localBenefitValue.LocalBenefitName,
                    Benefit = newBenefit,
                    Local_ID = localBenefitValue.Local_ID
                };
                _dbContext.Local_Benefits.Add(newLocal_Benefit);
            }




            await _dbContext.SaveChangesAsync();

            var benefitDto = await GetBenefit(newBenefit.Benefit_ID);

            return benefitDto;
        }

        public async Task<bool> DeleteBenefits(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var benefit = await _dbContext.Benefits.FindAsync(id);
                if (benefit == null)
                {
                    return false;
                }
                _dbContext.Benefits.Remove(benefit);
            }
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

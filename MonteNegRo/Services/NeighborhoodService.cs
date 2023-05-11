using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonteNegRo.Common;
using MonteNegRo.DBContext;
using MonteNegRo.Dtos.NeighborhoodDtos;
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
    public class NeighborhoodService : INeighborhoodService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        public NeighborhoodService(MyDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<NeighborhoodDto> neighborhoodDtos,
            PaginationFilter filter,
            int totalRecords)>
            GetAllNeighborhoodsPaged(NeighborhoodPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 40);

            var mainPredicate = PredicateBuilder.True<Neighborhood>();
            var neighborhoodsIQueryable = _dbContext.Neighborhoods.AsQueryable();
            if (query.Search != null & query.Search != "")
            {
                var local_Neighborhoods = await _dbContext.Local_Neighborhoods.Where(s => s.LocalNeighborhoodName.Contains(query.Search)).ToListAsync();
                var secondaryPredicate = PredicateBuilder.False<Neighborhood>();
                foreach (var local_Neighborhood in local_Neighborhoods)
                {
                    secondaryPredicate = secondaryPredicate.OR(s => s.Neighborhood_ID == local_Neighborhood.Neighborhood_ID);

                }
                mainPredicate = mainPredicate.AND(secondaryPredicate);

            }


            neighborhoodsIQueryable = neighborhoodsIQueryable
                .Where(mainPredicate)
                .OrderBy(s => s.City_ID)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);

            var totalRecords = await neighborhoodsIQueryable.CountAsync();
            var neighborhoods = await neighborhoodsIQueryable
                .Include(s => s.Local_Neighborhoods)
                .ThenInclude(s => s.Local)
                .ToListAsync();

            var neighborhoodDtos = new List<NeighborhoodDto>();
            foreach (var neighborhood in neighborhoods)
            {
                var neighborhoodDto = _mapper.Map<NeighborhoodDto>(neighborhood);
                neighborhoodDtos.Add(neighborhoodDto);
            }
            return (neighborhoodDtos, paginationFilter, totalRecords);
        }

        public async Task<NeighborhoodDto> GetNeighborhood(long id)
        {
            var neighborhood = await _dbContext.Neighborhoods
                .Include(s => s.Local_Neighborhoods)
                .ThenInclude(s => s.Local)
                .Where(s => s.Neighborhood_ID == id)
                .SingleOrDefaultAsync();
            var neighborhoodDto = _mapper.Map<NeighborhoodDto>(neighborhood);
            return neighborhoodDto;
        }

        public async Task<NeighborhoodDto> CreateNeighborhood(NeighborhoodCreateDto neighborhoodCreateDto)
        {
            var newNeighborhood = new Neighborhood()
            {
                City_ID = neighborhoodCreateDto.City_ID,
            };
            _dbContext.Neighborhoods.Add(newNeighborhood);

            foreach (var localNeighborhoodValue in neighborhoodCreateDto.Local_Neighborhoods)
            {
                var newLocal_Neighborhood = new Local_Neighborhood()
                {
                    LocalNeighborhoodName = localNeighborhoodValue.LocalNeighborhoodName,
                    Neighborhood = newNeighborhood,
                    Local_ID = localNeighborhoodValue.Local_ID
                };
                _dbContext.Local_Neighborhoods.Add(newLocal_Neighborhood);
            }
            await _dbContext.SaveChangesAsync();

            var neighborhoodDto = await GetNeighborhood(newNeighborhood.Neighborhood_ID);
            return neighborhoodDto;
        }

        public async Task<bool> DeleteNeighborhood(long id)
        {
            var neighborhood = await _dbContext.Neighborhoods.FindAsync(id);
            if (neighborhood == null)
            {
                return false;
            }
            _dbContext.Neighborhoods.Remove(neighborhood);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<NeighborhoodDto> UpdateNeighborhood(long id, NeighborhoodUpdateDto neighborhoodUpdateDto)
        {
            var neighborhood = await _dbContext.Neighborhoods.AsNoTracking()
                .Where(s => s.Neighborhood_ID == id)
                .SingleOrDefaultAsync();

            if (neighborhood == null)
            {
                return null;
            }

            var newNeighborhood = new Neighborhood()
            {
                Neighborhood_ID = id,
                City_ID = neighborhoodUpdateDto.City_ID,
            };
            _dbContext.Neighborhoods.Update(newNeighborhood);

            var local_Neighborhoods = await _dbContext.Local_Neighborhoods
                .Where(s => s.Neighborhood_ID == id)
                .ToListAsync();

            foreach (var localNeighborhood in local_Neighborhoods)
            {
                _dbContext.Local_Neighborhoods.Remove(localNeighborhood);
            }

            foreach (var localNeighborhoodValue in neighborhoodUpdateDto.Local_Neighborhoods)
            {
                var newLocal_Neighborhood = new Local_Neighborhood()
                {
                    LocalNeighborhoodName = localNeighborhoodValue.LocalNeighborhoodName,
                    Neighborhood = newNeighborhood,
                    Local_ID = localNeighborhoodValue.Local_ID
                };
                _dbContext.Local_Neighborhoods.Add(newLocal_Neighborhood);
            }



            await _dbContext.SaveChangesAsync();

            var neighborhoodDto = await GetNeighborhood(id);
            return neighborhoodDto;
        }

        public async Task<IEnumerable<NeighborhoodDto>> GetAllNeighborhoods()
        {
            var neighborhoods = await _dbContext.Neighborhoods.ToListAsync();
            var neighborhoodDtos = new List<NeighborhoodDto>();
            foreach (var neighborhood in neighborhoods)
            {
                var neighborhoodDto = _mapper.Map<NeighborhoodDto>(neighborhood);
                neighborhoodDtos.Add(neighborhoodDto);
            }
            return neighborhoodDtos;
        }
        public async Task<bool> DeleteNeighborhoods(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var neighborhood = await _dbContext.Neighborhoods.FindAsync(id);
                if (neighborhood == null)
                {
                    return false;
                }
                _dbContext.Neighborhoods.Remove(neighborhood);
            }
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

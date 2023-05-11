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
    public class CityService : ICityService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        public CityService(MyDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CityDto>> GetAllCities()
        {
            var cities = await GetAllCitiesWithAllInformation(_dbContext.Cities);

            var cityDtos = new List<CityDto>();
            foreach (var city in cities)
            {
                var cityDto = _mapper.Map<CityDto>(city);
                cityDtos.Add(cityDto);
            }
            return cityDtos;
        }
        private async Task<IEnumerable<City>> GetAllCitiesWithAllInformation(IQueryable<City> citiesIQueryable)
        {
            return await citiesIQueryable
                .Include(s => s.Local_Cities).ThenInclude(s => s.Local)
                .Include(s => s.City_Properties).ThenInclude(s => s.City)
                .ThenInclude(s => s.Neighborhoods).ThenInclude(s => s.Local_Neighborhoods)
                .ThenInclude(s => s.Local)
                .Include(s => s.Neighborhoods).ThenInclude(s => s.Local_Neighborhoods)
                .ThenInclude(s => s.Local)
                .ToListAsync();
        }
        private async Task<City> GetCityWithAllInformation(long id)
        {
            return await _dbContext.Cities
                .Include(s => s.Local_Cities).ThenInclude(s => s.Local)
                .Include(s => s.City_Properties).ThenInclude(s => s.City)
                .ThenInclude(s => s.Neighborhoods).ThenInclude(s => s.Local_Neighborhoods)
                .ThenInclude(s => s.Local)
                .Include(s => s.Neighborhoods).ThenInclude(s => s.Local_Neighborhoods)
                .ThenInclude(s => s.Local)
                .Where(s => s.City_ID == id)
                .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<CityDto>> GetCities(IEnumerable<long> ids)
        {
            var cities = new List<City>();
            foreach (var id in ids)
            {
                var city = await GetCityWithAllInformation(id);
                cities.Add(city);
            }
            var cityDtos = new List<CityDto>();
            foreach (var city in cities)
            {
                var cityDto = _mapper.Map<CityDto>(city);
                cityDtos.Add(cityDto);
            }
            return cityDtos;
        }
        public async Task<(IEnumerable<CityDto> cityDtos,
            PaginationFilter filter,
            int totalRecords)>
            GetAllCitiesPaged(CityPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 20);

            var mainPredicate = PredicateBuilder.True<City>();

            var citiesIQueryable = _dbContext.Cities.AsQueryable();

            if (query.Search != null & query.Search != "")
            {
                var local_Cities = await _dbContext.Local_Cities
                    .Where(s => s.LocalCityName
                    .Contains(query.Search))
                    .ToListAsync();
                var secondaryPredicate = PredicateBuilder.False<City>();

                foreach (var local_City in local_Cities)
                {
                    secondaryPredicate = secondaryPredicate.OR(s => s.City_ID == local_City.City_ID);

                }
                mainPredicate = mainPredicate.AND(secondaryPredicate);

            }


            citiesIQueryable = citiesIQueryable
                .Where(mainPredicate)
                .OrderBy(s => s.City_ID)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);

            var totalRecords = await citiesIQueryable.CountAsync();

            var cities = await GetAllCitiesWithAllInformation(citiesIQueryable);


            var cityDtos = new List<CityDto>();
            foreach (var city in cities)
            {
                var cityDto = _mapper.Map<CityDto>(city);
                cityDtos.Add(cityDto);
            }
            return (cityDtos, paginationFilter, totalRecords);
        }
        public async Task<CityDto> GetCity(long id)
        {
            var city = await GetCityWithAllInformation(id);

            if (city == null)
            {
                return null;
            }
            var cityDto = _mapper.Map<CityDto>(city);
            return cityDto;
        }
        public async Task<CityDto> CreateCity(CityCreateDto cityCreateDto)
        {
            var newCity = new City()
            {

            };
            _dbContext.Cities.Add(newCity);

            foreach (var localCityValue in cityCreateDto.Local_Cities)
            {
                var newLocal_City = new Local_City()
                {
                    LocalCityName = localCityValue.LocalCityName,
                    City = newCity,
                    Local_ID = localCityValue.Local_ID
                };
                _dbContext.Local_Cities.Add(newLocal_City);
            }



            await _dbContext.SaveChangesAsync();
            var cityDto = await GetCity(newCity.City_ID);
            return cityDto;
        }

        public async Task<bool> DeleteCity(long id)
        {
            var city = await _dbContext.Cities.FindAsync(id);
            if (city == null)
            {
                return false;
            }
            _dbContext.Cities.Remove(city);
            await _dbContext.SaveChangesAsync();

            return true;

        }
        public async Task<CityDto> UpdateCity(long id, CityUpdateDto cityUpdateDto)
        {
            var city = await _dbContext.Cities.AsNoTracking()
                .Where(s => s.City_ID == id)
                .SingleOrDefaultAsync();

            if (city == null)
            {
                return null;
            }

            var newCity = new City()
            {
                City_ID = id,
            };
            _dbContext.Cities.Update(newCity);

            var local_Properties = await _dbContext.Local_Cities
                .Where(s => s.City_ID == id)
                .ToListAsync();

            foreach (var local_Property in local_Properties)
            {
                _dbContext.Local_Cities.Remove(local_Property);
            }


            foreach (var localCityValue in cityUpdateDto.Local_Cities)
            {
                var newLocal_City = new Local_City()
                {
                    LocalCityName = localCityValue.LocalCityName,
                    City = newCity,
                    Local_ID = localCityValue.Local_ID
                };
                _dbContext.Local_Cities.Add(newLocal_City);
            }



            await _dbContext.SaveChangesAsync();


            var cityDto = await GetCity(id);
            return cityDto;
        }

        public async Task<bool> DeleteCities(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var city = await _dbContext.Cities.FindAsync(id);
                if (city == null)
                {
                    return false;
                }
                _dbContext.Cities.Remove(city);
            }
            await _dbContext.SaveChangesAsync();

            return true;
        }


    }
}

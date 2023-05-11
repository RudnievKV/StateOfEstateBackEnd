using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonteNegRo.Common;
using MonteNegRo.DBContext;
using MonteNegRo.Dtos.AdvertisementSettingDtos;
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
    public class AdvertisementSettingService : IAdvertisementSettingService
    {
        private readonly MyDBContext _dbContext;
        private readonly IMapper _mapper;
        public AdvertisementSettingService(IMapper mapper, MyDBContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AdvertisementSettingDto> GetAdvertisementSetting(long id)
        {
            var advertisementSetting = await _dbContext.AdvertisementSettings
                .Where(s => s.AdvertisementSetting_ID == id)
                .SingleOrDefaultAsync();
            if (advertisementSetting == null)
            {
                return null;
            }
            var advertisementSettingDto = _mapper.Map<AdvertisementSettingDto>(advertisementSetting);
            return advertisementSettingDto;
        }

        public async Task<(IEnumerable<AdvertisementSettingDto> advertisementSettingDtos,
            PaginationFilter filter,
            int totalRecords)>
            GetAllAdvertisementSettingsPaged(AdvertisementSettingPaginatedQuery query)
        {
            var paginationFilter = QueryParser.ParseQueryForPageFilters(query, 20);

            var advertisementSettingsIQueryable = _dbContext.AdvertisementSettings
                .OrderBy(s => s.AdvertisementSetting_ID)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);

            var totalRecords = await advertisementSettingsIQueryable.CountAsync();

            var advertisementSettings = await advertisementSettingsIQueryable
                .ToListAsync();

            var advertisementSettingDtos = new List<AdvertisementSettingDto>();
            foreach (var advertisementSetting in advertisementSettings)
            {
                var advertisementSettingDto = _mapper.Map<AdvertisementSettingDto>(advertisementSetting);
                advertisementSettingDtos.Add(advertisementSettingDto);
            }
            return (advertisementSettingDtos, paginationFilter, totalRecords);
        }

        public async Task<bool> DeleteAdvertisementSetting(long id)
        {
            var advertisementSetting = await _dbContext.AdvertisementSettings
                .Where(s => s.AdvertisementSetting_ID == id)
                .SingleOrDefaultAsync();
            if (advertisementSetting == null)
            {
                return false;
            }
            _dbContext.AdvertisementSettings.Remove(advertisementSetting);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<AdvertisementSettingDto> UpdateAdvertisementSetting(
            long id, AdvertisementSettingUpdateDto advertisementSettingUpdateDto)
        {
            var advertisementSetting = await _dbContext.AdvertisementSettings
                .AsNoTracking()
                .Where(s => s.AdvertisementSetting_ID != id)
                .SingleOrDefaultAsync();

            if (advertisementSetting == null)
            {
                return null;
            }

            var newAdvertisementSetting = new AdvertisementSetting()
            {
                AdvertisementSetting_ID = id,
                FacebookRent = advertisementSettingUpdateDto.FacebookRent,
                FacebookSale = advertisementSettingUpdateDto.FacebookSale,
                HomesOverseasSale = advertisementSettingUpdateDto.HomesOverseasSale,
                InstagramRent = advertisementSettingUpdateDto.InstagramRent,
                InstagramSale = advertisementSettingUpdateDto.InstagramSale,
                Property_ID = advertisementSetting.Property_ID,
                RealticAccountRent_ID = advertisementSettingUpdateDto.RealticAccountRent_ID,
                RealticAccountSale_ID = advertisementSettingUpdateDto.RealticAccountSale_ID
            };

            _dbContext.AdvertisementSettings.Update(newAdvertisementSetting);

            await _dbContext.SaveChangesAsync();
            var advertisementSettingDto = await GetAdvertisementSetting(id);
            return advertisementSettingDto;
        }
    }
}

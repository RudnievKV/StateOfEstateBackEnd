using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Dtos.AdvertisementSettingDtos;
using MonteNegRo.Dtos.PartnerDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonteNegRo.Services.Interfaces
{
    public interface IAdvertisementSettingService
    {
        public Task<AdvertisementSettingDto> GetAdvertisementSetting(long id);
        public Task<(IEnumerable<AdvertisementSettingDto> advertisementSettingDtos, PaginationFilter filter, int totalRecords)> GetAllAdvertisementSettingsPaged(AdvertisementSettingPaginatedQuery query);
        public Task<AdvertisementSettingDto> UpdateAdvertisementSetting(long id, AdvertisementSettingUpdateDto advertisementSettingUpdateDto);
        public Task<bool> DeleteAdvertisementSetting(long id);
    }
}

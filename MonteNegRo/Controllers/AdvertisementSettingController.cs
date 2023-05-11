using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Common;
using MonteNegRo.Services.Interfaces;
using System.Linq;
using System;
using System.Security.Claims;
using MonteNegRo.Dtos.RealticAccountDtos;
using MonteNegRo.Services;
using MonteNegRo.Wrappers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MonteNegRo.Dtos.AdvertisementSettingDtos;
using MonteNegRo.Dtos.Queries;
using Microsoft.AspNetCore.Authorization;

namespace MonteNegRo.Controllers
{
    [Route("api/advertisementsettings")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner, Admin")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class AdvertisementSettingController : ControllerBase
    {
        private readonly IAdvertisementSettingService _advertisementSettingService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public AdvertisementSettingController(IAdvertisementSettingService advertisementSettingService)
        {
            _advertisementSettingService = advertisementSettingService;
        }
        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<AdvertisementSettingDto>>>>
            GetAllAdvertisementSettingsPaged(
            [FromQuery] AdvertisementSettingPaginatedQuery query)
        {
            try
            {
                var result = await _advertisementSettingService.GetAllAdvertisementSettingsPaged(query);
                var advertisementSettingDtos = result.advertisementSettingDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;


                var pagedResponse = new PagedResponse<IEnumerable<AdvertisementSettingDto>>(
                    advertisementSettingDtos,
                    paginationFilter.PageNumber,
                    paginationFilter.PageSize,
                    totalRecords);

                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdvertisementSettingDto>> GetRealticAccount(long id)
        {
            try
            {
                var advertisementSettingDto = await _advertisementSettingService.GetAdvertisementSetting(id);
                if (advertisementSettingDto == null)
                {
                    return NotFound("Specified advertisementSetting does not exist.");
                }

                return Ok(advertisementSettingDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AdvertisementSettingDto>> UpdateRealticAccount(
            long id,
            [FromBody] AdvertisementSettingUpdateDto advertisementSettingUpdateDto)
        {
            try
            {
                var advertisementSettingDto = await _advertisementSettingService.UpdateAdvertisementSetting(id, advertisementSettingUpdateDto);
                if (advertisementSettingDto == null)
                {
                    return NotFound("Specified realticAccount does not exist.");
                }

                return Ok(advertisementSettingDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRealticAccount(long id)
        {
            try
            {
                var result = await _advertisementSettingService.DeleteAdvertisementSetting(id);
                if (result == false)
                {
                    return NotFound("Specified advertisementSetting does not exist.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
    }
}

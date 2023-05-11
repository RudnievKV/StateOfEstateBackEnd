using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Common;
using MonteNegRo.Dtos.BenefitDtos;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.LocalDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Services;
using MonteNegRo.Services.Interfaces;
using MonteNegRo.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonteNegRo.Controllers
{
    [Route("api/cities")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner, Admin")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        // GET: api/<CityController>
        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<CityDto>>>> GetAllCitiesPaged(
            [FromQuery] CityPaginatedQuery query)
        {
            try
            {
                var result = await _cityService.GetAllCitiesPaged(query);
                var cityDtos = result.cityDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;


                var pagedResponse = new PagedResponse<IEnumerable<CityDto>>(
                    cityDtos,
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
        public async Task<ActionResult<CityDto>> GetCity(long id)
        {
            try
            {
                var cityDto = await _cityService.GetCity(id);
                if (cityDto == null)
                {
                    return NotFound("Specified city does not exist.");
                }

                return Ok(cityDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpGet("cities-by-id")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities([FromQuery] IEnumerable<long> ids)
        {
            try
            {
                var cityDtos = await _cityService.GetCities(ids);
                if (cityDtos == null)
                {
                    return NotFound("Specified cities do not exist.");
                }

                return Ok(cityDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [Route("all")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetAllCities()
        {
            try
            {
                var cityDtos = await _cityService.GetAllCities();

                return Ok(cityDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // POST api/<CityController>
        [HttpPost]
        public async Task<ActionResult<CityDto>> CreateCity([FromBody] CityCreateDto cityCreateDto)
        {
            try
            {
                var cityDto = await _cityService.CreateCity(cityCreateDto);

                return CreatedAtAction(nameof(CreateCity), cityDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CityDto>> UpdateCity(long id, [FromBody] CityUpdateDto cityUpdateDto)
        {
            try
            {
                var cityDto = await _cityService.UpdateCity(id, cityUpdateDto);
                if (cityDto == null)
                {
                    return NotFound("Specified city does not exist.");
                }

                return Ok(cityDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(long id)
        {
            try
            {
                var result = await _cityService.DeleteCity(id);
                if (result == false)
                {
                    return NotFound("Specified city does not exist.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        // DELETE api/<CityController>/5
        [HttpDelete]
        public async Task<IActionResult> DeleteCities([FromQuery] IEnumerable<long> ids)
        {
            try
            {
                var result = await _cityService.DeleteCities(ids);
                if (result == false)
                {
                    return NotFound("Specified cities do not exist.");
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

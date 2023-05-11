using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Common;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Filters;
using MonteNegRo.Services;
using MonteNegRo.Services.Interfaces;
using MonteNegRo.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonteNegRo.Controllers
{
    [Route("api/properties")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner, Admin")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public PropertyController(IPropertyService propertyService)
        {
            this._propertyService = propertyService;
        }
        // GET: api/<PropertyController>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<PropertyDto>>>> GetAllPropertiesPaged(
            [FromQuery] PropertyPaginatedQuery query)
        {
            try
            {
                var result = await _propertyService.GetAllPropertiesPaged(query);
                var propertyDtos = result.propertyDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;


                var pagedResponse = new PagedResponse<IEnumerable<PropertyDto>>(
                    propertyDtos,
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
        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetAllProperties()
        {
            try
            {
                var propertyDtos = await _propertyService.GetAllProperties();

                return Ok(propertyDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [AllowAnonymous]
        // GET api/<PropertyController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDto>> GetProperty(long id)
        {
            try
            {
                var propertyDto = await _propertyService.GetProperty(id);

                return Ok(propertyDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [AllowAnonymous]
        // GET api/<PropertyController>/5
        [HttpGet("/api/properties/similar/{id}")]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetSimilarProperties(long id)
        {
            try
            {
                var propertyDtos = await _propertyService.GetSimilarProperties(id);

                return Ok(propertyDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        // GET api/<PropertyController>/search/
        [AllowAnonymous]
        [HttpGet("/api/properties/search/")]
        public async Task<ActionResult<PagedResponse<IEnumerable<PropertyDto>>>> SearchProperties(
            [FromQuery] PropertySearchPaginatedQuery query)
        {
            try
            {

                var result = await _propertyService.SearchProperties(query);
                var propertyDtos = result.propertyDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;


                var pagedResponse = new PagedResponse<IEnumerable<PropertyDto>>(
                    propertyDtos,
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

        // POST api/<PropertyController>
        [HttpPost]
        [RequestSizeLimit(50 * 25 * 1024 * 1024)]
        public async Task<ActionResult<PropertyDto>> CreateProperty(
            [FromForm] PropertyCreateDto propertyCreateDto)
        {
            try
            {
                var propertyDto = await _propertyService.CreateProperty(propertyCreateDto);

                return CreatedAtAction(nameof(CreateProperty), propertyDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // PUT api/<PropertyController>/5
        [RequestSizeLimit(50 * 25 * 1024 * 1024)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(long id,
            [FromForm] PropertyUpdateDto propertyUpdateDto)
        {
            try
            {
                var propertyDto = await _propertyService.UpdateProperty(id, propertyUpdateDto);

                return Ok(propertyDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // DELETE api/<PropertyController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(long id)
        {
            try
            {
                await _propertyService.DeleteProperty(id);

                return Ok();
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProperties([FromQuery] IEnumerable<long> ids)
        {
            try
            {
                var result = await _propertyService.DeleteProperties(ids);
                if (result == false)
                {
                    return NotFound("Specified properties do not exist.");
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

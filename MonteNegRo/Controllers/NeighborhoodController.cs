using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Dtos.LocalDtos;
using MonteNegRo.Services;
using MonteNegRo.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MonteNegRo.Dtos.NeighborhoodDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Wrappers;
using MonteNegRo.Dtos.UserDtos;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Data;
using MonteNegRo.Common;
using MonteNegRo.Dtos.Queries;
using MonteNegRo.Dtos.CounterpartyDtos;

namespace MonteNegRo.Controllers
{
    [Route("api/neighborhoods")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner, Admin")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class NeighborhoodController : ControllerBase
    {
        private readonly INeighborhoodService _neighborhoodService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public NeighborhoodController(INeighborhoodService neighborhoodService)
        {
            _neighborhoodService = neighborhoodService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<NeighborhoodDto>>>> GetAllNeighborhoodsPaged(
            [FromQuery] NeighborhoodPaginatedQuery query)
        {
            try
            {
                var result = await _neighborhoodService.GetAllNeighborhoodsPaged(query);
                var neighborhoodDtos = result.neighborhoodDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;


                var pagedResponse = new PagedResponse<IEnumerable<NeighborhoodDto>>(
                    neighborhoodDtos,
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
        public async Task<ActionResult<IEnumerable<CounterpartyDto>>> GetAllNeighborhoods()
        {
            try
            {
                var neighborhoodDtos = await _neighborhoodService.GetAllNeighborhoods();

                return Ok(neighborhoodDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<NeighborhoodDto>> GetNeighborhood(long id)
        {
            try
            {
                var neighborhoodDto = await _neighborhoodService.GetNeighborhood(id);
                if (neighborhoodDto == null)
                {
                    return NotFound("Specified neighborhood does not exist.");
                }

                return Ok(neighborhoodDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpPost]
        public async Task<ActionResult<NeighborhoodDto>> CreateNeighborhood(
            [FromBody] NeighborhoodCreateDto neighborhoodCreateDto)
        {
            try
            {
                var neighborhoodDto = await _neighborhoodService.CreateNeighborhood(neighborhoodCreateDto);

                return CreatedAtAction(nameof(CreateNeighborhood), neighborhoodDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<NeighborhoodDto>> UpdateNeighborhood(long id,
            [FromBody] NeighborhoodUpdateDto neighborhoodUpdateDto)
        {
            try
            {
                var neighborhoodDto = await _neighborhoodService.UpdateNeighborhood(id, neighborhoodUpdateDto);
                if (neighborhoodDto == null)
                {
                    return NotFound("Specified neighborhood does not exist.");
                }

                return Ok(neighborhoodDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNeighborhood(long id)
        {
            try
            {
                var result = await _neighborhoodService.DeleteNeighborhood(id);
                if (result == false)
                {
                    return NotFound("Specified neighborhood does not exist.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteNeighborhoods([FromQuery] IEnumerable<long> ids)
        {
            try
            {
                var result = await _neighborhoodService.DeleteNeighborhoods(ids);
                if (result == false)
                {
                    return NotFound("Specified neighborhoods do not exist.");
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Common;
using MonteNegRo.Dtos.UserTypeDtos;
using MonteNegRo.Services;
using MonteNegRo.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MonteNegRo.Dtos.LocalDtos;
using MonteNegRo.Models;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Wrappers;
using MonteNegRo.Dtos.NeighborhoodDtos;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using MonteNegRo.Dtos.Queries;

namespace MonteNegRo.Controllers
{
    [Route("api/locals")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner, Admin")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class LocalController : ControllerBase
    {
        private readonly ILocalService _localService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public LocalController(ILocalService localService)
        {
            _localService = localService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<LocalDto>>>> GetAllLocals(
            [FromQuery] LocalPaginatedQuery query)
        {
            try
            {
                var result = await _localService.GetAllLocals(query);
                var localDtos = result.localDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;


                var pagedResponse = new PagedResponse<IEnumerable<LocalDto>>(
                    localDtos,
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

        [HttpGet("{id}")]
        public async Task<ActionResult<LocalDto>> GetLocal(long id)
        {
            try
            {
                var localDto = await _localService.GetLocal(id);
                if (localDto == null)
                {
                    return NotFound("Specified local does not exist.");
                }

                return Ok(localDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpPost]
        public async Task<ActionResult<LocalDto>> CreateLocal([FromBody] LocalCreateDto localCreateDto)
        {
            try
            {
                var localDto = await _localService.CreateLocal(localCreateDto);

                return CreatedAtAction(nameof(CreateLocal), localDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LocalDto>> UpdateLocal(long id,
            [FromBody] LocalUpdateDto localUpdateDto)
        {
            try
            {
                var userTypeDto = await _localService.UpdateLocal(id, localUpdateDto);
                if (userTypeDto == null)
                {
                    return NotFound("Specified local does not exist.");
                }

                return Ok(userTypeDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocal(long id)
        {
            try
            {
                var result = await _localService.DeleteLocal(id);
                if (result == false)
                {
                    return NotFound("Specified local does not exist.");
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

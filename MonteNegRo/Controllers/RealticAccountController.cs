using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Common;
using MonteNegRo.Services.Interfaces;
using System.Linq;
using System;
using System.Security.Claims;
using MonteNegRo.Dtos.PartnerDtos;
using MonteNegRo.Services;
using MonteNegRo.Wrappers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MonteNegRo.Dtos.RealticAccountDtos;
using MonteNegRo.Dtos.Queries;
using Microsoft.AspNetCore.Authorization;

namespace MonteNegRo.Controllers
{
    [Route("api/realticaccounts")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner, Admin")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class RealticAccountController : ControllerBase
    {
        private readonly IRealticAccountService _realticAccountService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public RealticAccountController(IRealticAccountService realticAccountService)
        {
            _realticAccountService = realticAccountService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<RealticAccountDto>>>> GetAllRealticAccountsPaged(
            [FromQuery] RealticAccountPaginatedQuery query)
        {
            try
            {
                var result = await _realticAccountService.GetAllRealticAccountsPaged(query);
                var realticAccountDtos = result.realticAccountDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;


                var pagedResponse = new PagedResponse<IEnumerable<RealticAccountDto>>(
                    realticAccountDtos,
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
        public async Task<ActionResult<RealticAccountDto>> GetRealticAccount(long id)
        {
            try
            {
                var realticAccountDto = await _realticAccountService.GetRealticAccount(id);
                if (realticAccountDto == null)
                {
                    return NotFound("Specified realticAccount does not exist.");
                }

                return Ok(realticAccountDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RealticAccountDto>>> GetAllRealticAccounts()
        {
            try
            {
                var partnerDtos = await _realticAccountService.GetAllRealticAccounts();

                return Ok(partnerDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // POST api/<CityController>
        [HttpPost]
        public async Task<ActionResult<RealticAccountDto>> CreateRealticAccount(
            [FromBody] RealticAccountCreateDto realticAccountCreateDto)
        {
            try
            {
                var realticAccountDto = await _realticAccountService.CreateRealticAccount(realticAccountCreateDto);

                return CreatedAtAction(nameof(CreateRealticAccount), realticAccountDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<RealticAccountDto>> UpdateRealticAccount(
            long id,
            [FromBody] RealticAccountUpdateDto realticAccountUpdateDto)
        {
            try
            {
                var realticAccountDto = await _realticAccountService.UpdateRealticAccount(id, realticAccountUpdateDto);
                if (realticAccountDto == null)
                {
                    return NotFound("Specified realticAccount does not exist.");
                }

                return Ok(realticAccountDto);
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
                var result = await _realticAccountService.DeleteRealticAccount(id);
                if (result == false)
                {
                    return NotFound("Specified realticAccount does not exist.");
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

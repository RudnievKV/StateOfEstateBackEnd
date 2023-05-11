using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Common;
using MonteNegRo.Services.Interfaces;
using System.Linq;
using System;
using System.Security.Claims;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Services;
using MonteNegRo.Wrappers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MonteNegRo.Dtos.CounterpartyDtos;
using MonteNegRo.Dtos.Queries;
using Microsoft.AspNetCore.Authorization;

namespace MonteNegRo.Controllers
{
    [Route("api/counterparties")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner, Admin")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class CounterpartyController : ControllerBase
    {
        private readonly ICounterpartyService _counterpartyService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public CounterpartyController(ICounterpartyService counterpartyService)
        {
            _counterpartyService = counterpartyService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<CounterpartyDto>>>> GetAllCounterpartiesPaged(
            [FromQuery] CounterpartyPaginatedQuery query)
        {
            try
            {
                var result = await _counterpartyService.GetAllCounterpartiesPaged(query);
                var counterpartyDtos = result.counterpartyDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;


                var pagedResponse = new PagedResponse<IEnumerable<CounterpartyDto>>(
                    counterpartyDtos,
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
        public async Task<ActionResult<CounterpartyDto>> GetCounterparty(long id)
        {
            try
            {
                var counterpartyDto = await _counterpartyService.GetCounterparty(id);
                if (counterpartyDto == null)
                {
                    return NotFound("Specified counterparty does not exist.");
                }

                return Ok(counterpartyDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CounterpartyDto>>> GetAllCounterparties()
        {
            try
            {
                var counterpartyDtos = await _counterpartyService.GetAllCounterparties();

                return Ok(counterpartyDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // POST api/<CityController>
        [HttpPost]
        public async Task<ActionResult<CounterpartyDto>> CreateCounterparty([FromBody] CounterpartyCreateDto counterpartyCreateDto)
        {
            try
            {
                var counterpartyDto = await _counterpartyService.CreateCounterparty(counterpartyCreateDto);

                return CreatedAtAction(nameof(CreateCounterparty), counterpartyDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CounterpartyDto>> UpdateCounterparty(
            long id,
            [FromBody] CounterpartyUpdateDto counterpartyUpdateDto)
        {
            try
            {
                var counterpartyDto = await _counterpartyService.UpdateCounterparty(id, counterpartyUpdateDto);
                if (counterpartyDto == null)
                {
                    return NotFound("Specified counterparty does not exist.");
                }

                return Ok(counterpartyDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounterparty(long id)
        {
            try
            {
                var result = await _counterpartyService.DeleteCounterparty(id);
                if (result == false)
                {
                    return NotFound("Specified counterparty does not exist.");
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Common;
using MonteNegRo.Dtos.BenefitDtos;
using MonteNegRo.Dtos.CityDtos;
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
    [Route("api/benefits")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner, Admin")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class BenefitController : ControllerBase
    {
        private readonly IBenefitService _benefitService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public BenefitController(IBenefitService benefitService)
        {
            _benefitService = benefitService;
        }
        // GET: api/<BenefitController>
        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<BenefitDto>>>> GetAllBenefitsPaged(
            [FromQuery] BenefitPaginatedQuery query)
        {
            try
            {
                var result = await _benefitService.GetAllBenefitsPaged(query);
                var benefitDtos = result.benefitDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;


                var pagedResponse = new PagedResponse<IEnumerable<BenefitDto>>(
                    benefitDtos,
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
        [AllowAnonymous]
        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BenefitDto>>> GetAllBenefits()
        {
            try
            {
                var benefitDtos = await _benefitService.GetAllBenefits();

                return Ok(benefitDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        // GET api/<BenefitController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BenefitDto>> GetBenefit(long id)
        {
            try
            {
                var benefitDto = await _benefitService.GetBenefit(id);
                if (benefitDto == null)
                {
                    return NotFound("Specified benefit does not exist.");
                }

                return Ok(benefitDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // POST api/<BenefitController>
        [HttpPost]
        public async Task<ActionResult<BenefitDto>> CreateBenefit([FromBody] BenefitCreateDto benefitCreateDto)
        {
            try
            {
                var benefitDto = await _benefitService.CreateBenefit(benefitCreateDto);

                return CreatedAtAction(nameof(CreateBenefit), benefitDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // PUT api/<BenefitController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BenefitDto>> UpdateBenefit(long id,
            [FromBody] BenefitUpdateDto benefitUpdateDto)
        {
            try
            {
                var benefitDto = await _benefitService.UpdateBenefit(id, benefitUpdateDto);
                if (benefitDto == null)
                {
                    return NotFound("Specified benefit does not exist.");
                }

                return Ok(benefitDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // DELETE api/<BenefitController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await _benefitService.DeleteBenefit(id);
                if (result == false)
                {
                    return NotFound("Specified benefit does not exist.");
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
        public async Task<IActionResult> DeleteBenefits([FromQuery] IEnumerable<long> ids)
        {
            try
            {
                var result = await _benefitService.DeleteBenefits(ids);
                if (result == false)
                {
                    return NotFound("Specified benefits do not exist.");
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

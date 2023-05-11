using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Common;
using MonteNegRo.Services.Interfaces;
using System.Linq;
using System;
using System.Security.Claims;
using MonteNegRo.Dtos.NotificationDtos;
using MonteNegRo.Services;
using MonteNegRo.Wrappers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MonteNegRo.Dtos.PartnerDtos;
using MonteNegRo.Dtos.Queries;
using Microsoft.AspNetCore.Authorization;

namespace MonteNegRo.Controllers
{
    [Route("api/partners")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner, Admin")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<PartnerDto>>>> GetAllPartnersPaged(
            [FromQuery] PartnerPaginatedQuery query)
        {
            try
            {
                var result = await _partnerService.GetAllPartnersPaged(query);
                var partnerDtos = result.partnerDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;


                var pagedResponse = new PagedResponse<IEnumerable<PartnerDto>>(
                    partnerDtos,
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
        public async Task<ActionResult<PartnerDto>> GetPartner(long id)
        {
            try
            {
                var partnerDto = await _partnerService.GetPartner(id);
                if (partnerDto == null)
                {
                    return NotFound("Specified partner does not exist.");
                }

                return Ok(partnerDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartnerDto>>> GetAllPartners()
        {
            try
            {
                var partnerDtos = await _partnerService.GetAllPartners();

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
        public async Task<ActionResult<PartnerDto>> CreatePartner([FromBody] PartnerCreateDto partnerCreateDto)
        {
            try
            {
                var partnerDto = await _partnerService.CreatePartner(partnerCreateDto);

                return CreatedAtAction(nameof(CreatePartner), partnerDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PartnerDto>> UpdatePartner(
            long id,
            [FromBody] PartnerUpdateDto partnerUpdateDto)
        {
            try
            {
                var partnerDto = await _partnerService.UpdatePartner(id, partnerUpdateDto);
                if (partnerDto == null)
                {
                    return NotFound("Specified partner does not exist.");
                }

                return Ok(partnerDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartner(long id)
        {
            try
            {
                var result = await _partnerService.DeletePartner(id);
                if (result == false)
                {
                    return NotFound("Specified partner does not exist.");
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
        public async Task<IActionResult> DeletePartners([FromQuery] IEnumerable<long> ids)
        {
            try
            {
                var result = await _partnerService.DeletePartners(ids);
                if (result == false)
                {
                    return NotFound("Specified partners do not exist.");
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

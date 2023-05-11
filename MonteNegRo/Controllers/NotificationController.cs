using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Common;
using MonteNegRo.Services.Interfaces;
using System.Linq;
using System;
using System.Security.Claims;
using MonteNegRo.Dtos.CounterpartyDtos;
using MonteNegRo.Services;
using MonteNegRo.Wrappers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MonteNegRo.Dtos.NotificationDtos;
using MonteNegRo.Dtos.Queries;
using Microsoft.AspNetCore.Authorization;

namespace MonteNegRo.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner, Admin")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<NotificationDto>>>> GetAllNotificationsPaged(
            [FromQuery] NotificationPaginatedQuery query)
        {
            try
            {
                var result = await _notificationService.GetAllNotificationsPaged(query);
                var notificationDtos = result.notificationDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;


                var pagedResponse = new PagedResponse<IEnumerable<NotificationDto>>(
                    notificationDtos,
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
        public async Task<ActionResult<NotificationDto>> GetNotification(long id)
        {
            try
            {
                var notificationDto = await _notificationService.GetNotification(id);
                if (notificationDto == null)
                {
                    return NotFound("Specified notification does not exist.");
                }

                return Ok(notificationDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetAllNotifications()
        {
            try
            {
                var notificationDtos = await _notificationService.GetAllNotifications();

                return Ok(notificationDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // POST api/<CityController>
        [HttpPost]
        public async Task<ActionResult<NotificationDto>> CreateNotification(
            [FromBody] NotificationCreateDto notificationCreateDto)
        {
            try
            {
                var notificationDto = await _notificationService.CreateNotification(notificationCreateDto);

                return CreatedAtAction(nameof(CreateNotification), notificationDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<NotificationDto>> UpdateNotification(
            long id,
            [FromBody] NotificationUpdateDto notificationUpdateDto)
        {
            try
            {
                var notificationDto = await _notificationService.UpdateNotification(id, notificationUpdateDto);
                if (notificationDto == null)
                {
                    return NotFound("Specified notification does not exist.");
                }

                return Ok(notificationDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(long id)
        {
            try
            {
                var result = await _notificationService.DeleteNotification(id);
                if (result == false)
                {
                    return NotFound("Specified notification does not exist.");
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
        public async Task<IActionResult> DeleteNotifications([FromQuery] IEnumerable<long> ids)
        {
            try
            {
                var result = await _notificationService.DeleteNotifications(ids);
                if (result == false)
                {
                    return NotFound("Specified notifications do not exist.");
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

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
using MonteNegRo.Dtos.UserDtos;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Wrappers;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using MonteNegRo.Dtos.Queries;

namespace MonteNegRo.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<UserDto>>>> GetAllUsers(
            [FromQuery] UserPaginatedQuery query)
        {
            try
            {
                var result = await _userService.GetAllUserPaged(query);
                var userDtos = result.userDtos;
                var paginationFilter = result.filter;
                var totalRecords = result.totalRecords;



                var pagedResponse = new PagedResponse<IEnumerable<UserDto>>(
                    userDtos,
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
        public async Task<ActionResult<UserDto>> GetUser(long id)
        {
            try
            {
                var userDto = await _userService.GetUser(id);
                if (userDto == null)
                {
                    return NotFound("Specified user does not exist.");
                }

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            try
            {
                var userDto = await _userService.CreateUser(userCreateDto);
                if (userDto == null)
                {
                    return BadRequest("Username is not unique.");
                }

                return CreatedAtAction(nameof(CreateUser), userDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CityDto>> UpdateUser(long id,
            [FromBody] UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await GetUser(id);
                if (user == null)
                {
                    return NotFound("Specified user does not exist.");
                }
                var userDto = await _userService.UpdateUser(id, userUpdateDto);
                if (userDto == null)
                {
                    return BadRequest("Username is not unique.");
                }

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                if (result == false)
                {
                    return NotFound("Specified user does not exist.");
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
        public async Task<IActionResult> DeleteUsers([FromQuery] IEnumerable<long> ids)
        {
            try
            {
                var result = await _userService.DeleteUsers(ids);
                if (result == false)
                {
                    return NotFound("Specified users do not exist.");
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

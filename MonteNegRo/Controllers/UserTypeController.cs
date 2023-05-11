using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Common;
using MonteNegRo.Dtos.CityDtos;
using MonteNegRo.Services;
using MonteNegRo.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MonteNegRo.Dtos.UserTypeDtos;
using System.Linq;
using System.Security.Claims;
using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Cloud.Translation.V2;

namespace MonteNegRo.Controllers
{
    [Route("api/usertypes")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public UserTypeController(IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTypeDto>>> GetAllUserTypes()
        {
            try
            {
                var userTypeDtos = await _userTypeService.GetAllUserTypes();


                return Ok(userTypeDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserTypeDto>> GetUserTypes(long id)
        {
            try
            {
                var userTypeDto = await _userTypeService.GetUserType(id);
                if (userTypeDto == null)
                {
                    return NotFound("Specified usertype does not exist.");
                }

                return Ok(userTypeDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserTypeDto>> CreateUserType(
            [FromBody] UserTypeCreateDto userTypeCreateDto)
        {
            try
            {
                var userTypeDto = await _userTypeService.CreateUserType(userTypeCreateDto);

                return CreatedAtAction(nameof(CreateUserType), userTypeDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CityDto>> UpdateUserType(long id,
            [FromBody] UserTypeUpdateDto userTypeUpdateDto)
        {
            try
            {
                var userTypeDto = await _userTypeService.UpdateUserType(id, userTypeUpdateDto);
                if (userTypeDto == null)
                {
                    return NotFound("Specified usertype does not exist.");
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
        public async Task<IActionResult> DeleteUserType(long id)
        {
            try
            {
                var result = await _userTypeService.DeleteUserType(id);
                if (result == false)
                {
                    return NotFound("Specified usertype does not exist.");
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

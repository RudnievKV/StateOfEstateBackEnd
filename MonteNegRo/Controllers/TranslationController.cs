using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonteNegRo.Common;
using MonteNegRo.Dtos.PropertyDtos;
using MonteNegRo.Dtos.TranslationDtos;
using MonteNegRo.Services;
using MonteNegRo.Services.Interfaces;
using MonteNegRo.Wrappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MonteNegRo.Controllers
{
    [Route("api/translation")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Owner, Admin")]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService _translateService;
        private long User_ID => Int32.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private string UserType => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;
        public TranslationController(ITranslationService translateService)
        {
            _translateService = translateService;
        }
        [HttpPost]
        public async Task<ActionResult<TranslationDto>> GetTranslations([FromBody] TranslationInputDto translationInputDto)
        {
            try
            {
                var translations = await _translateService.GetTranslations(translationInputDto);

                return Ok(translations);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

    }
}

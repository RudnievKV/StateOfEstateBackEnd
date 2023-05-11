using AuthorizationApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System;
using AuthorizationApi.Models;
using AuthorizationApi.Services.Interfaces;

namespace AuthorizationApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Produces("application/json")]
    [AllowAnonymous]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService repo;
        private readonly IOptions<AuthOptions> authOptions;
        public AuthController(IAuthService repo, IOptions<AuthOptions> authOptions)
        {
            this.repo = repo;
            this.authOptions = authOptions;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(Login request)
        {
            try
            {
                var account = await AuthenticateUser(request.Username, request.Password);

                if (account != null)
                {
                    var token = GenerateJWT(account);

                    return Ok(new
                    {
                        access_token = token,
                        user_type = account.UserType,
                        user_ID = account.ID,
                    });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }

        }
        [Route("check")]
        [HttpPost]
        public async Task<IActionResult> CheckUser(Login request)
        {
            try
            {
                var user = await repo.GetUser(request.Username, request.Password);


                if (user != null)
                {
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        private async Task<Account> AuthenticateUser(string email, string password)
        {
            var user = await repo.GetUser(email, password);
            if (user != null)
            {
                var userType = await repo.GetUserType(user);

                var account = new Account()
                {
                    Username = email,
                    Password = password,
                    ID = user.User_ID,
                    UserType = userType
                };
                return account;
            }
            return null;
        }

        private string GenerateJWT(Account user)
        {
            var authParams = authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString())
            };

            claims.Add(new Claim("role", user.UserType));

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

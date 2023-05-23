using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate = authenticate ??
                throw new ArgumentNullException(nameof(authenticate));
            _configuration = configuration;
        }

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi =  true)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult> CreateUser([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.RegisterUser(userInfo.Email, userInfo.Password);
            if (result)
            {
                return Ok($"User {userInfo.Email} was create successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid creation attempt.");
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);
            if (result)
            {
                return GenerateToken(userInfo);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenerateToken(LoginModel userInfo)
        {
            //User declaration
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("2b891g1!", "Any Array"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // generate private key for sing the token
            var privateKey = new SymmetricSecurityKey(
                                 Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            // Generate virtual sign
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            // Time expiration definity
            var expiration = DateTime.UtcNow.AddMinutes(10);

            // Token Generate
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}

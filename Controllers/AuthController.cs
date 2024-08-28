using FlexiSourceCodingTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlexiSourceCodingTest.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "Auth")]
    public class AuthController : Controller
    {
        private IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("GetBearerToken")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<GenerateBearerTokenReturn>>> GetBearerToken()
        {
            var token = GenerateToken("FlexiSourceCodingTest");

            return Ok(new GenerateBearerTokenReturn
            {
                statusCode = "00",
                statusMessage = "Success",
                token = token
            });
        }

        #region "   Private Methods     "
        private string GenerateToken(string Username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Username)
            };

            int minute = int.Parse(_config["Jwt:ExpireInMinute"]);

            var token = new JwtSecurityToken
                (
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(minute),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

    }
}

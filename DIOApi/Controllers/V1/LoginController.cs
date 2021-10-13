using DIOApi.Data_Transfer_Objects;
using DIOApi.Services.Data_Access_Layer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DIOApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginDBContext repository;
        private readonly IConfiguration _configuration;

        public LoginController(LoginDBContext dBContext, IConfiguration configuration)
        {
            repository = dBContext;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = repository.loginInfo.Where(data => data.UserId == loginModel.UserId && data.AccessKey == loginModel.AccessKey).FirstOrDefault();
                if (result is not null)
                {
                    var jwtToken = BuildToken(loginModel);
                    return Ok(jwtToken);
                }
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }
        }

        private string BuildToken(LoginModel loginModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT_Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, loginModel.UserId)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            /*
            var tokenExpiration = DateTime.UtcNow.AddHours(6);

            var _claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, loginModel.UserId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, "AlmeidaDev.DioAPI")
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT_Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                        claims: _claims,
                        signingCredentials: credentials,
                        expires: tokenExpiration
                    );
            return new TokenModel { Expiration = tokenExpiration, Token = new JwtSecurityTokenHandler().WriteToken(token) };*/
        }
    }
}

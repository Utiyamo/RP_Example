using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RPApi.Models;
using RPApi.MongoDB.DAO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RPApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _configuration;

        private readonly UsersService _userService = new UsersService();
        private readonly ConfigurationService _confService = new ConfigurationService();

        public UsersController(ILogger<UsersController> logger, 
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Users> getUsers()
        {
            return _userService.getUsers().Result;
        }

        [HttpPost]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] Users model)
        {
            var result = await _userService.CreateUserAsync(model);
            if ((bool)(result?.Id.HasValue))
            {
                return BuildToken(model);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] Users model)
        {
            var result = await _userService.getUserByLogin(model.Username, model.Password);

            if ((bool)(result?.Id.HasValue))
            {
                return BuildToken(model);
            }
            else
            {
                ModelState.AddModelError(String.Empty, result.Message);
                return BadRequest(ModelState);  
            }
        }

        private UserToken BuildToken(Users usuario)
        {
            var jwt = _confService.getConfiguration(usuario.CodEmpresa).Result?.jwtModel;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Email),
                new Claim("Corporation", jwt.NameCorporation),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"].ToString(),
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}

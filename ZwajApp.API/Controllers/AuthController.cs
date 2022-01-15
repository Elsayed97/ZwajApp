using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.API.Data;
using ZwajApp.API.Models;
using ZwajApp.API.ViewModels;

namespace ZwajApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo , IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterViewModel userVM)
        {

            if (ModelState.IsValid)
            {
                userVM.userName = userVM.userName.ToLower();
                if (await _repo.isUserExists(userVM.userName))
                {
                    return BadRequest("هذا المستخدم مسجل من قبل");
                }
                var userToCreate = new User()
                {
                    UserName = userVM.userName,
                    Email = userVM.Email
                };
                var CreatedUser = await _repo.Register(userToCreate, userVM.password);
                return StatusCode(StatusCodes.Status201Created);

            }else{
                return BadRequest(ModelState);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel userVM){

            var userFromRepo = await _repo.Login(userVM.userName.ToLower(),userVM.password);
            if(userFromRepo == null) return Unauthorized();
            var claims = new [] {
                new Claim(ClaimTypes.NameIdentifier , userFromRepo.Id),
                new Claim(ClaimTypes.Name , userFromRepo.UserName ),
                new Claim(ClaimTypes.Email , userFromRepo.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSetteings:Token").Value));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new {
                  token = tokenHandler.WriteToken(token)
            });


        }

    }
}
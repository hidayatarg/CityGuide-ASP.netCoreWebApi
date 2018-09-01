using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SehirRehber.API.Data;
using SehirRehber.API.Dtos;
using SehirRehber.API.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace SehirRehber.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        //Injection
        private IAuthRepository _authRepository;

        // Private Key from appSetting JSON Configuration
        private IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            // If Registering Record already avaliable
            if (await  _authRepository.UserExits(userForRegisterDto.UserName))
            {
                ModelState.AddModelError("UserName","Username already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName
            };

            // Hashed password is added 
            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var user = await _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);

            // User not avaliable
            if (user==null)
            {
                return Unauthorized();
            }

            // If user avaliable 
            // Send a token (Will do all operation with that token)
            var tokenHandler = new JwtSecurityTokenHandler();

            // We Generte token with private key in appsetting.json
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor=new SecurityTokenDescriptor
            {
                // Data we want store in Token work for all .net
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),
                    
                }),
                //Token validity
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials =  new SigningCredentials(new SymmetricSecurityKey(key)
                    , SecurityAlgorithms.HmacSha512Signature)
            };

            //creating Token according to tokenDescriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Signing In user and give token
            return Ok(tokenString);
        }
        
    }
}
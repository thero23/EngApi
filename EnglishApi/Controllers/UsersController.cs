using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using English.Database.Data;
using English.Database.Models;
using English.Services.Interfaces;
using EnglishApi.Logger;
using EnglishApi.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public UsersController(IUserService service, ILoggerManager logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("", Name = "GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.FindAllUsers(false);
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = (await _service.FindUserByCondition(x => x.Id == id, false)).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        

        //POST: api/users/auth/signin
        [HttpPost]
        [Route("signIn")]
        public async Task<IActionResult> Signin([FromForm] string login, [FromForm] string password)
        {
            var identity = await GetIdentity(login, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                login = identity.Name
            };

            return Ok(response);
        }

       

     
        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            var task = await Task.Run(async () =>
            {
                var user = (await _service.FindUserByCondition(x => x.Login == login && x.Password == password, false)).Include(x => x.UserRole).FirstOrDefault();
                if (user == null) return null;
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserRole.Name)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            });

            return task;
        }


        [HttpGet]
        [Route("test/{id}", Name = "test")]
        public async Task<IActionResult> GetUserOnTest(Guid id)
        {
            var user = (await _service.FindUserByCondition(x => x.Id == id, true)).Include(x => x.UserRole).FirstOrDefault();

            
            
            return Ok(user);
        }


        [HttpPost]
        [Route("testupd")]
        public async Task<IActionResult> UpdateUserTest([FromBody]User user)
        {

            var role = (await _service.FindUserRoleByCondition(x => x.Name == "admin", true)).FirstOrDefault();
            
            user.UserRoleId = role.Id;
            user.UserRole = role;

            _service.UpdateUser(user);
            
            await _service.Save();
            
            return CreatedAtRoute(nameof(GetUserOnTest), user.Id, user);
        }

    }
}

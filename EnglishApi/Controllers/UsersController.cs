using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using English.Database.Models;
using English.Services.DTOs;
using English.Services.Interfaces;
using EnglishApi.Logger;
using EnglishApi.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public  IActionResult GetAllUsers()
        {
            var users =_service.FindAllUsers(false).Include(x=>x.UserRole);
            var usersDto = _mapper.Map<IEnumerable<UserWithRoleDto>>(users);

            return Ok(usersDto);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}", Name = "GetUserById")]
        public  IActionResult GetUserById(Guid id)
        {
            var user =  _service.FindUserByCondition(x => x.Id == id, false).Include(c=>c.UserRole).FirstOrDefault();
            var userWithRole =_mapper.Map<UserWithRoleDto>(user);
            if (userWithRole == null)
            {
                return NotFound();
            }

            return Ok(userWithRole);
        }

        

        
        [HttpPost]
        [Route("auth/signIn")]
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
            var task = await Task.Run( () =>
            {
                var user = _service.FindUserByCondition(x => x.Login == login && x.Password == password, false).Include(x => x.UserRole).FirstOrDefault();
                var userWithRole = _mapper.Map<UserWithRoleDto>(user);

                if (userWithRole == null) return null;
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userWithRole.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, userWithRole.UserRoleName)
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
        public  IActionResult GetUserOnTest(Guid id)
        {
            var user = _service.FindUserByCondition(x => x.Id == id, true).FirstOrDefault();

            
            
            return Ok(user);
        }


        [HttpPost]
        [Route("testupd")]
        public async Task<IActionResult> UpdateUserTest([FromBody]User user)
        {

            var role = _service.FindUserRoleByCondition(x => x.Name == "admin", true).FirstOrDefault();
            
            user.UserRoleId = role.Id;
            user.UserRole = role;

            _service.UpdateUser(user);
            
            await _service.Save();
            
            return CreatedAtRoute(nameof(GetUserOnTest), user.Id, user);
        }

    }
}

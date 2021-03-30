using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using English.Services.Interfaces;
using EnglishApi.ActionFilters;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IAuthenticationManager _authManager;
        public AuthenticationController(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IAuthenticationManager authManager)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }


        [HttpGet, Authorize]
        [Route("check")]
        public async Task<IActionResult> Check()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var isTeacher = new {teacher = User.IsInRole("Administrator") || User.IsInRole("Teacher")};
            
            return Ok(isTeacher);
        }

        [HttpGet, Authorize]
        [Route("checkadmin")]
        public async Task<IActionResult> CheckAdmin()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var isAdmin = new { admin = User.IsInRole("Administrator") };

            return Ok(isAdmin);
        }

        [HttpGet, Authorize]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            var teachers =await _userManager.GetUsersInRoleAsync("Teacher");
            var admins = await _userManager.GetUsersInRoleAsync("Administrator");

            var allUsers = new {users = users, teachers = teachers, admins = admins};
            return Ok(allUsers);
        }

        [HttpGet, Authorize]
        [Route("getCurrentUser")]
        public async Task<IActionResult> getCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var role = await _userManager.GetRolesAsync(user);
            return Ok(new {user = user, role = role});
        }
        [HttpPost]
        [Route("changeRole/{userId}/{role}/{oldRole}")]
        public async Task<IActionResult> ChangeRole(string userId, string role, string oldRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.RemoveFromRoleAsync(user, oldRole);
            await _userManager.AddToRoleAsync(user, role);
            return Ok();
        }

        [HttpPost]
        [Route("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
           
            var user = _mapper.Map<User>(userForRegistration);
            
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            
            
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles); 
            return StatusCode(201);
        }


        [HttpPost]
        [Route("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto
            user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
                return Unauthorized();
            }
            return Ok(new { Token = await _authManager.CreateToken()});
        }


    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using English.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ISectionService _service;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public SectionsController(ISectionService service, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
        {
            _userManager = userManager;
            _service = service;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet, Authorize]
        [Route("", Name = "GetAllSections")]
        public async Task<IActionResult> GetAllSections()
        {
            
           //var role = User.FindFirstValue(ClaimTypes.Role);
         
            var user =await _userManager.FindByNameAsync(User.Identity.Name);
            var isTeacher = User.IsInRole("Administrator") || User.IsInRole("Teacher");

            var sections = isTeacher ? await _service.FindAllSections(false): await _service.FindSectionsByUser(user);

            return Ok(sections);
            
        }


        [HttpGet]
        [Route("{id}", Name = "GetSectionById")]
        public async Task<IActionResult> GetSectionById(Guid id)
        {
            
            var section =(await _service.FindSectionsByCondition(p=>p.Id == id,false)).FirstOrDefault();
           
            return Ok(section);
        }

        [HttpPost]
        [Route("", Name = "AddSection")]
        public async Task<IActionResult> AddSection([FromBody] Section section)
        {
            await _service.CreateSection(section);
            await _service.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteSection")]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            var  section =(await _service.FindSectionsByCondition(p=>p.Id==id,true)).FirstOrDefault();
            if (section == null)
            {
                return NotFound();
            }

            _service.DeleteSection(section);
            await _service.Save();
            return NoContent();
        }

        [HttpPut]
        [Route("", Name = "UpdateSection")]
        public async Task<IActionResult> UpdateSection([FromBody] Section section)
        {
            _service.UpdateSection(section);
            await _service.Save();
            return Ok(section);
        }


        //подразделы

        [HttpGet]
        [Route("{sectionId}/subsections")]
        public async Task<IActionResult> GetSubsectionsFromSection(Guid sectionId)
        {
            if (!(await _service.IsSectionExist(sectionId)))
            {

                return NotFound();
            }
            var subsections = _service.FindSubsectionsInSection(sectionId,false);
            return Ok(subsections);
        }

   

        [HttpPost]
        [Route("{sectionId}/subsections/{subsectionId}", Name = "AddSubsectionToSection")]
        public async Task<IActionResult> AddSubsectionToSection(Guid sectionId, Guid subsectionId)
        {
            if (!(await _service.IsSectionExist(sectionId)))
            {
                return NotFound();
            }

            if (!_service.IsSubsectionExist(subsectionId))
            {
                return NotFound();
            }

            if (await _service.IsSubsectionInSection(subsectionId, sectionId))
            {
                return BadRequest();
            }
            _service.AddSubsectionToSection(subsectionId,sectionId);
            await _service.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("{sectionId}/subsections/{subsectionId}", Name = "DeleteSubsectionFromSection")]
        public async Task<IActionResult> DeleteSubsectionFromSection(Guid subsectionId, Guid sectionId)
        {
            if (!(await _service.IsSectionExist(sectionId)))
            {
                return BadRequest();

            }

            if (!_service.IsSubsectionExist(subsectionId))
            {
                return BadRequest();
            }

            if (!(await _service.IsSubsectionInSection(subsectionId, sectionId)))
            {
                return BadRequest();

            }
            await _service.DeleteSubsectionFromSection(subsectionId);
            await _service.Save();
            return Ok();
        }

        //пользователи

        [HttpGet]
        [Route("{id}/users", Name = "GetUsersBySectionId")]
        public  IActionResult GetUsersBySectionId(Guid id)
        {
            var users = _userManager.GetUsersInRoleAsync("User").Result.Where(u=> _service.IsHasAccess(u.Id, id).Result);
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}/notusers", Name = "GetNotInSectionUsers")]
        public IActionResult GetNotInSectionUsers(Guid id)
        {
            var users = _userManager.GetUsersInRoleAsync("User").Result.Where(u =>
                !(_service.IsHasAccess(u.Id, id).Result));
            return Ok(users);
        }
        [HttpPost]
        [Route("{sectionId}/users/{userId}", Name = "AddUserToSection")]
        public async Task<IActionResult> AddUserToSection(Guid sectionId, string userId)
        {
            if (!(await _service.IsSectionExist(sectionId)))
            {
                return NotFound();

            }

            if (!_service.IsUserExist(userId))
            {
                return NotFound();
            }

            if (_service.IsHasAccess(userId, sectionId).Result)
            {
                return BadRequest();
            }

            await _service.AddUserToSection(userId, sectionId);
            await _service.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("{sectionId}/users/{userId}", Name = "DeleteUserFromSection")]
        public async Task<IActionResult> DeleteUserFromSection(Guid sectionId, string userId)
        {
            if (!(await _service.IsSectionExist(sectionId)))
            {
                return NotFound();

            }

            if (!_service.IsUserExist(userId))
            {
                return NotFound();
            }

            if (!_service.IsHasAccess(userId, sectionId).Result)
            {
                return BadRequest();
            }
            await _service.DeleteUserFromSection(userId, sectionId);
            await _service.Save();
            return Ok();
        }

        
        // словари


        [HttpGet]
        [Route("{sectionId}/dictionaries")]
        public  async Task<IActionResult> GetDictionariesFromSection(Guid sectionId)
        {
            if (!(await _service.IsSectionExist(sectionId)))
            {

                return NotFound();
            }
            var dictionaries = _service.FindDictionariesInSection(sectionId);
            return Ok(dictionaries);
        }

        [HttpGet]
        [Route("{sectionId}/notdictionaries")]
        public async Task<IActionResult> GetDictionariesNotInSection(Guid sectionId)
        {
            if (!(await _service.IsSectionExist(sectionId)))
            {

                return NotFound();
            }
            var dictionaries = _service.FindDictionariesNotInSection(sectionId);
           
            return Ok(dictionaries);
        }

        [HttpPost]
        [Route("{sectionId}/dictionaries/{dictionaryId}", Name = "AddDictionaryToSection")]
        public async Task<IActionResult> AddDictionaryToSection(Guid sectionId, Guid dictionaryId)
        {

            
            if (!(await _service.IsSectionExist(sectionId)))
            {
                return NotFound();

            }

            if (!(await _service.IsDictionaryExist(dictionaryId)))
            {
                return NotFound();
            }

            if (_service.IsDictionaryInSection(dictionaryId, sectionId).Result)
            {
                BadRequest();
            }

            await _service.AddDictionaryToSection(dictionaryId, sectionId);
            await _service.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("{sectionId}/dictionaries/{dictionaryId}", Name = "DeleteDictionaryFromSection")]
        public async Task<IActionResult> DeleteDictionaryFromSection(Guid dictionaryId, Guid sectionId)
        {
            if (!(await _service.IsSectionExist(sectionId)))
            {
                return NotFound();

            }

            if (!(await _service.IsDictionaryExist(dictionaryId)))
            {
                return NotFound();
            }

            if (!(await _service.IsDictionaryInSection(dictionaryId, sectionId)))
            {
                return NotFound();

            }
            await _service.DeleteDictionaryFromSection(dictionaryId, sectionId);
            await _service.Save();
            return Ok();
        }



    }
}

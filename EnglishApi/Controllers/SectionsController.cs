using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using English.Database.Data.Interfaces;
using English.Database.Models;
using English.Services.Interfaces;
using EnglishApi.Logger;
using Microsoft.AspNetCore.Http;
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
        private readonly ISectionService _service;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;



        public SectionsController(ISectionService service, ILoggerManager logger, IMapper mapper)
        {

            _service = service;
            _logger = logger;
            _mapper = mapper;

        }


        [HttpGet]
        [Route("", Name = "GetAllSections")]
        public IActionResult GetAllSections()
        {
            var sections =  _service.FindAllSections(false);

            return Ok(sections);
        }


        [HttpGet]
        [Route("{id}", Name = "GetSectionById")]
        public IActionResult GetSectionById(Guid id)
        {
            
            var section = _service.FindSectionByCondition(p=>p.Id == id,false);
           
            return Ok(section);
        }

        [HttpPost]
        [Route("", Name = "AddSection")]
        public async Task<IActionResult> AddSection([FromBody] Section section)
        {
            await _service.CreateSection(section);
            await _service.Save();
            return CreatedAtRoute(nameof(GetSectionById), section.Id, section);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteSection")]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            var  section = _service.FindSectionByCondition(p=>p.Id==id,true).FirstOrDefault();
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

        [HttpPost]
        [Route("{sectionId}/subsections/{subsectionId}", Name = "AddSubsectionToSection")]
        public async Task<IActionResult> AddSubsectionToSection(Guid sectionId, Guid subsectionId)
        {
            if (!_service.IsSectionExist(sectionId))
            {
                return NotFound();
            }

            if (!_service.IsSubsectionExist(subsectionId))
            {
                return NotFound();
            }

            if (_service.IsSubsectionInSection(subsectionId, sectionId))
            {
                return BadRequest();
            }
            _service.AddSubsectionToSection(subsectionId,sectionId);
            await _service.Save();
            return Ok();
        }

        


        [HttpPost]
        [Route("{sectionId}/users/{userId}", Name = "AddUserToSection")]
        public async Task<IActionResult> AddUserToSection(Guid sectionId, Guid userId)
        {
            if (!_service.IsSectionExist(sectionId))
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
        public async Task<IActionResult> DeleteUserFromSection(Guid sectionId, Guid userId)
        {
            if (!_service.IsSectionExist(sectionId))
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

        [HttpPost]
        [Route("{sectionId}/dictionaries/{dictionaryId}", Name = "AddDictionaryToSection")]
        public async Task<IActionResult> AddDictionaryToSection(Guid sectionId, Guid dictionaryId)
        {

            
            if (!_service.IsSectionExist(sectionId))
            {
                return NotFound();

            }

            if (!_service.IsDictionaryExist(dictionaryId))
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



    }
}

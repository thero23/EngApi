using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using EnglishApi.Data.Interfaces;
using EnglishApi.Logger;
using EnglishApi.Models;
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
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;



        public SectionsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {

            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }


        [HttpGet]
        [Route("", Name = "GetAllSections")]
        public async Task<IActionResult> GetAllSections()
        {
            var sections = (await _repository.Section.FindAll(false));

            return Ok(sections);
        }


        [HttpGet]
        [Route("{id}", Name = "GetSectionById")]
        public async Task<IActionResult> GetSectionById(Guid id)
        {
            
            var section = (await _repository.Section.FindByCondition(p=>p.Id == id,false));
           
            return Ok(section);
        }

        [HttpPost]
        [Route("", Name = "AddSection")]
        public async Task<IActionResult> AddSection([FromBody] Section section)
        {
            await _repository.Section.Create(section);
            await _repository.Save();
            return CreatedAtRoute(nameof(GetSectionById), section.Id, section);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteSection")]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            var  section =(await _repository.Section.FindByCondition(p=>p.Id==id,true)).FirstOrDefault();
            if (section == null)
            {
                return NotFound();
            }

            _repository.Section.Delete(section);
            await _repository.Save();
            return NoContent();
        }

        [HttpPut]
        [Route("", Name = "UpdateSection")]
        public async Task<IActionResult> UpdateSection([FromBody] Section section)
        {
            _repository.Section.Update(section);
            await _repository.Save();
            return Ok(section);
        }

        [HttpPost]
        [Route("{sectionId}/subsections/{subsectionId}", Name = "AddSubsectionToSection")]
        public async Task<IActionResult> AddSubsectionToSection(Guid sectionId, Guid subsectionId)
        {
            var section = (await _repository.Section.FindByCondition(p=>p.Id == sectionId,false)).FirstOrDefault();
            var subsection = (await _repository.Subsection.FindByCondition(p=>p.Id==subsectionId,false)).FirstOrDefault();

            if (section == null)
            {
                return NotFound();

            }

            if (subsection == null)
            {
                return NotFound();
            }

            if (_repository.Section.IsSubsectionInSection(subsection, section))
            {
                return BadRequest();
            }
            _repository.Section.AddSubsectionToSection(subsection,section);
            await _repository.Save();
            return Ok();
        }

        


        [HttpPost]
        [Route("{sectionId}/users/{userId}", Name = "AddUserToSection")]
        public async Task<IActionResult> AddUserToSection(Guid sectionId, Guid userId)
        {
            
            var section = (await _repository.Section.FindByCondition(p => p.Id == sectionId, false)).FirstOrDefault();
            var user = (await _repository.User.FindByCondition(p => p.Id == userId, false)).FirstOrDefault();

            if (section == null)
            {
                return NotFound();

            }

            if (user == null)
            {
                return NotFound();
            }

            if (_repository.Section.IsHasAccess(user, section).Result)
            {
                BadRequest();
            }

            await _repository.Section.AddUserToSection(user, section);
            await _repository.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("{sectionId}/users/{userId}", Name = "DeleteUserFromSection")]
        public async Task<IActionResult> DeleteUserFromSection(Guid sectionId, Guid userId)
        {
            var section = (await _repository.Section.FindByCondition(p => p.Id == sectionId, false)).FirstOrDefault();
            var user = (await _repository.User.FindByCondition(p => p.Id ==userId, false)).FirstOrDefault();

            if (section == null)
            {
                return NotFound();

            }

            if (user == null)
            {
                return NotFound();
            }

            if (!_repository.Section.IsHasAccess(user, section).Result)
            {
                return BadRequest();
            }
            await _repository.Section.DeleteUserFromSection(user, section);
            await _repository.Save();
            return Ok();
        }

        [HttpPost]
        [Route("{sectionId}/dictionaries/{dictionaryId}", Name = "AddDictionaryToSection")]
        public async Task<IActionResult> AddDictionaryToSection(Guid sectionId, Guid dictionaryId)
        {

            var section = (await _repository.Section.FindByCondition(p => p.Id == sectionId, false)).FirstOrDefault();
            var dictionary = (await _repository.Dictionary.FindByCondition(p => p.Id == dictionaryId, false)).FirstOrDefault();

            if (section == null)
            {
                return NotFound();

            }

            if (dictionary == null)
            {
                return NotFound();
            }

            if (_repository.Section.IsDictionaryInSection(dictionary, section).Result)
            {
                BadRequest();
            }

            await _repository.Section.AddDictionaryToSection(dictionary, section);
            await _repository.Save();
            return Ok();
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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
            var sections = (await _repository.Section.FindAll(false)).Include(p=>p.Subsections);

            return Ok(sections);
        }


        [HttpGet]
        [Route("{id}", Name = "GetSectionById")]
        public async Task<IActionResult> GetSectionById(Guid id)
        {
            //убрать инклюды  и переопределить и методы на них
            var section = (await _repository.Section.FindByCondition(p=>p.Id == id,false)).Include(p=>p.Subsections);
           
            return Ok(section);
        }

        [HttpGet]
        [Route("subsections", Name = "GetAllSubsections")]
        public async Task<IActionResult> GetAllSubsections()
        {
         
            var sections = (await _repository.Subsection.FindAll(false)).Include(p=>p.Section);

            return Ok(sections);
        }

        [HttpGet]
        [Route("subsections/{id}", Name = "GetSubsectionById")]
        public async Task<IActionResult> GetSubsectionById(Guid id)
        {
            var subsection = (await _repository.Subsection.FindByCondition(p => p.Id == id, false)).Include(p=>p.Section);
            

            return Ok(subsection);
        }
    }
}

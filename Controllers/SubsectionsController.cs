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

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubsectionsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;



        public SubsectionsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {

            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("", Name = "GetAllSubsections")]
        public async Task<IActionResult> GetAllSubsections()
        {

            var sections = (await _repository.Subsection.FindAll(false));

            return Ok(sections);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubsectionById")]
        public async Task<IActionResult> GetSubsectionById(Guid id)
        {
            var subsection = (await _repository.Subsection.FindByCondition(p => p.Id == id, false));


            return Ok(subsection);
        }

        [HttpPost]
        [Route("", Name = "AddSubsection")]
        public async Task<IActionResult> AddSection([FromBody] Subsection subsection)
        {
            await _repository.Subsection.Create(subsection);
            await _repository.Save();
            return CreatedAtRoute(nameof(GetSubsectionById), subsection.Id, subsection);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteSubsection")]
        public async Task<IActionResult> DeleteSubsection(Guid id)
        {
            var subsection = (await _repository.Subsection.FindByCondition(p => p.Id == id, true)).FirstOrDefault();
            if (subsection == null)
            {
                return NotFound();
            }

            _repository.Subsection.Delete(subsection);
            await _repository.Save();
            return NoContent();
        }

        [HttpPut]
        [Route("", Name = "UpdateSubsection")]
        public async Task<IActionResult> UpdateSubsection([FromBody] Subsection subsection)
        {
            _repository.Subsection.Update(subsection);
            await _repository.Save();
            return Ok(subsection);
        }
    }
}

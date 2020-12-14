using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using English.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubsectionsController : ControllerBase
    {
        private readonly ISubsectionService _service;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;



        public SubsectionsController(ISubsectionService service, ILoggerManager logger, IMapper mapper)
        {

            _service = service;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("", Name = "GetAllSubsections")]
        public  IActionResult GetAllSubsections()
        {

            var sections = _service.FindAllSubsections(false);

            return Ok(sections);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubsectionById")]
        public IActionResult GetSubsectionById(Guid id)
        {
            var subsection = _service.FindSubsectionByCondition(p => p.Id == id, false);


            return Ok(subsection);
        }

        [HttpPost]
        [Route("", Name = "AddSubsection")]
        public async Task<IActionResult> AddSection([FromBody] Subsection subsection)
        {
            await _service.CreateSubsection(subsection);
            await _service.Save();
            return CreatedAtRoute(nameof(GetSubsectionById), subsection.Id, subsection);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteSubsection")]
        public async Task<IActionResult> DeleteSubsection(Guid id)
        {
            var subsection =  _service.FindSubsectionByCondition(p => p.Id == id, true).FirstOrDefault();
            if (subsection == null)
            {
                return NotFound();
            }

            _service.DeleteSubsection(subsection);
            await _service.Save();
            return NoContent();
        }

        [HttpPut]
        [Route("", Name = "UpdateSubsection")]
        public async Task<IActionResult> UpdateSubsection([FromBody] Subsection subsection)
        {
            _service.UpdateSubsection(subsection);
            await _service.Save();
            return Ok(subsection);
        }
    }
}

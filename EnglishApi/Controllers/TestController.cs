using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using English.Services.Interfaces;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IWordDictionaryService _service;
        private readonly ILoggerManager _logger;

        public TestController(IWordDictionaryService service, ILoggerManager logger)
        {

            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("", Name = "GetAllDictionaries2")]
        public IActionResult GetAllDictionaries()
        {
           
              
            
            
            var sections = _service.FindAllDictionaries(false);

            return Ok(sections);
        }
    }
}

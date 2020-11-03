using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Data.Interfaces;
using EnglishApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionariesController : ControllerBase
    {
        private readonly IBaseRepository<Word> _wordRepo;

        public DictionariesController(IBaseRepository<Word> wordRepo)
        {
            _wordRepo = wordRepo;
        }

        [HttpGet]
        [Route("words")]
        public ActionResult<IEnumerable<Word>> GetAllWords()
        {
            return Ok(_wordRepo.GetAll());
        }

        [HttpGet]
        [Route("words/{id}", Name = "GetWordById")]
        public ActionResult<Word> GetWordById(Guid id)
        {
            return Ok(_wordRepo.GetById(id));
        }

        [HttpPost]
        [Route("words", Name = "AddWord")]
        public async Task<ActionResult> AddWord([FromBody] Word word)
        {
            await _wordRepo.Create(word);
            return CreatedAtRoute(nameof(GetWordById), new {word.Id }, word);

        }


    }
}

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
        public async Task<IActionResult> AddWord([FromBody] Word word)
        {
            if (word == null)
            {
                return NotFound();
            }
            await _wordRepo.Create(word);
            return CreatedAtRoute(nameof(GetWordById), new {word.Id }, word);

        }

        [HttpDelete]
        [Route("words/{id}", Name = "DeleteWord")]
        public async Task<IActionResult> DeleteWord(Guid id)
        {
            var item =  _wordRepo.GetById(id);
            if (item == null)
            {
                return BadRequest();
            }
            await _wordRepo.Delete(item);
            return Ok();
        }

        [HttpPut]
        [Route("words/{id}", Name = "UpdateWord")]
        public async Task<IActionResult> UpdateWord(Guid id, [FromBody] Word word)
        {
            var item = _wordRepo.GetById(id);
            if (word == null)
            {
                return BadRequest();
            }

            if (item == null)
            {
                return NotFound();
            }
            
            
            await _wordRepo.Update(id,word);
            return Ok();
        }

        
    }
}

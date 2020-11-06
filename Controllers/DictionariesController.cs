using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishApi.Contracts;
using EnglishApi.Data;
using EnglishApi.Data.Interfaces;
using EnglishApi.Data.Repositories;
using EnglishApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionariesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        


        public DictionariesController(IRepositoryManager repository)
        {

            _repository = repository;
           
        }


        //Words
        // api/dictionaries/words/...



        [HttpGet]
        [Route("words")]
        public ActionResult<IEnumerable<Word>> GetAllWords()
        {
            var items = _repository.Word.FindAll(false);
             return Ok(items);

            
        }

        [HttpGet]
        [Route("words/{id}", Name = "GetWordById")]
        public ActionResult<Word> GetWordById(Guid id)
        {
            return Ok(_repository.Word.FindByCondition(p=>p.Id == id, false));
        }

        [HttpPost]
        [Route("words", Name = "AddWord")]
        public IActionResult AddWord([FromBody] Word word)
        {
            if (word == null)
            {
                return NotFound();
            }
            _repository.Word.Create(word);
            _repository.Save();
            return CreatedAtRoute(nameof(GetWordById), new { word.Id }, word);

        }

        [HttpDelete]
        [Route("words/{id}", Name = "DeleteWord")]
        public IActionResult DeleteWord(Guid id)
        {
            var item = _repository.Word.FindByCondition(p=>p.Id == id,true);
            if (item == null)
            {
                return BadRequest();
            }
            _repository.Word.Delete(item.First());
            _repository.Save();
            return Ok();
        }

        [HttpPut]
        [Route("words", Name = "UpdateWord")]
        public IActionResult UpdateWord([FromBody] Word word)
        {
            if (word == null)
            {
                return BadRequest();
            }
            _repository.Word.Update(word);
            _repository.Save();
            return Ok();
        }


        //Dictionary
        // api/dictionaries/...


        [HttpGet]
        [Route("", Name = "GetAllDictionaries")]
        public ActionResult<IEnumerable<Dictionary>> GetAllDictionaries()
        {
            return Ok(_repository.Dictionary.FindAll(false));
        }

        [HttpGet]
        [Route("{id}", Name = "GetDictionaryById")]
        public ActionResult<Dictionary> GetDictionaryById(Guid id)
        {
            return Ok(_repository.Dictionary.FindByCondition(p=>p.Id==id,false));
        }

        [HttpPost]
        [Route("", Name = "AddDictionary")]
        public IActionResult AddDictionary([FromBody] Dictionary dictionary)
        {
            if (dictionary == null)
            {
                return NotFound();
            }
            _repository.Dictionary.Create(dictionary);
            return CreatedAtRoute(nameof(GetDictionaryById), new { dictionary.Id }, dictionary);

        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteDictionary")]
        public IActionResult DeleteDictionary(Guid id)
        {
            var item = _repository.Dictionary.FindByCondition(p => p.Id == id, true).FirstOrDefault();
            if (item == null)
            {
                return BadRequest();
            }
            _repository.Dictionary.Delete(item);
            return Ok();
        }

        //DictionaryWord
        //api/dictionary/dictionary-word/..


        [HttpGet]
        [Route("{id}/words", Name = "GetWordsFromDictionary")]
        public ActionResult<IEnumerable<Word>> GetWordsFromDictionary(Guid id)
        {
            var item = _repository.Dictionary.FindByCondition(p => p.Id == id, false).FirstOrDefault();

            if (item == null)
            {
                return NotFound();
            }

            return Ok(_repository.DictionaryWord.GetWordsFromDictionary(item));

        }

        [HttpPost]
        [Route("{dictionaryId}/words/{wordId}", Name = "AddWordToDictionary")]
        public IActionResult AddWordToDictionary(Guid dictionaryId, Guid wordId)
        {
            var dictionary = _repository.Dictionary.FindByCondition(p=>p.Id == dictionaryId, false).FirstOrDefault();
            var word = _repository.Word.FindByCondition(p => p.Id == wordId, false).FirstOrDefault();

            if (word == null || dictionary == null)
            {
                return NotFound();
            }

            if (_repository.DictionaryWord.IsWordInDictionary(word, dictionary))
            {
                return BadRequest();
            }

            _repository.DictionaryWord.AddWordToDictionary(word,dictionary);
            _repository.Save();
            return Ok();

        }


        [HttpDelete]
        [Route("{dictionaryId}/words/{wordId}", Name = "RemoveWordFromDictionary")]
        public IActionResult RemoveWordFromDictionary(Guid wordId, Guid dictionaryId)
        {
            var dictionary = _repository.Dictionary.FindByCondition(p => p.Id == dictionaryId, false).FirstOrDefault();
            var word = _repository.Word.FindByCondition(p => p.Id == wordId, false).FirstOrDefault();

            if (word == null || dictionary == null)
            {
                return NotFound();
            }
            if (!_repository.DictionaryWord.IsWordInDictionary(word, dictionary))
            {
                return BadRequest();
            }

            _repository.DictionaryWord.RemoveWordFromDictionary(word, dictionary);
            _repository.Save();
            return NoContent();
        }
    }
}

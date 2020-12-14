using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using English.Services.DTOs;
using English.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;


namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionariesController : ControllerBase
    {
        // private readonly IRepositoryManager _repository;
        private readonly IWordDictionaryService _service;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;



        public DictionariesController(IWordDictionaryService service, ILoggerManager logger, IMapper mapper)
        {

           // _repository = repository;
           _service = service; 
           _logger = logger; 
           _mapper = mapper;

        }


        [HttpGet]
        [Route("words")]
        public  IActionResult GetAllWords()
        {

            var words =  _service.FindAllWords(false);
            var wordsDto = _mapper.Map<IEnumerable<WordGetDto>>(words);
            return Ok(wordsDto);

            
        }

        [HttpGet]
        [Route("words/{id}", Name = "GetWordById")]
        public IActionResult GetWordById(Guid id)
        {
            var word = _service.FindWordByCondition(p => p.Id == id, false).FirstOrDefault();
            
            if (word == null)
            {
                _logger.LogInfo($"Word with Id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var wordDto = _mapper.Map<WordGetDto>(word);
            return Ok(wordDto);
        }

        [HttpPost]
        [Route("words", Name = "AddWord")]
        public async Task<IActionResult> AddWord([FromBody] WordCreateDto wordDto)
        {
           
            var word = _mapper.Map<Word>(wordDto);
            await _service.CreateWord(word);
            await _service.Save();
            return CreatedAtRoute(nameof(GetWordById), new { word.Id }, word);

        }

        [HttpDelete]
        [Route("words/{id}", Name = "DeleteWord")]
        public async Task<IActionResult> DeleteWord(Guid id)
        {
            var item = _service.FindWordByCondition(p=>p.Id == id,true).FirstOrDefault();
            if (item == null)
            {
                return BadRequest();
            } 
            _service.DeleteWord(item);
            await _service.Save();
            return Ok();
        }

        [HttpPut]
        [Route("words", Name = "UpdateWord")]
        public async Task<IActionResult> UpdateWord([FromBody] WordUpdateDto wordDto)
        {
            if (wordDto == null)
            {
                return BadRequest();
            }

            var word = _mapper.Map<Word>(wordDto);
            _service.UpdateWord(word);
            await _service.Save();
            return Ok();
        }


        //Dictionary
        // api/dictionaries/...


        [HttpGet]
        [Route("", Name = "GetAllDictionaries")]
        public  IActionResult GetAllDictionaries()
        {
            var dictionaries =_service.FindAllDictionaries(false);
            var dictionariesDto = _mapper.Map<IEnumerable<DictionaryGetDto>>(dictionaries);
            return Ok(dictionariesDto);
        }

        [HttpGet]
        [Route("{id}", Name = "GetDictionaryById")]
        public  IActionResult GetDictionaryById(Guid id)
        {
            var item = _service.FindDictionaryByCondition(p => p.Id == id, false);
            if (item == null)
            {
                _logger.LogInfo($"Dictionary with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("", Name = "AddDictionary")]
        public async Task<IActionResult> AddDictionary([FromBody] DictionaryCreateDto dictionaryDto)
        {
            if (dictionaryDto == null)
            {
                return NotFound();
            }
            var dictionary = _mapper.Map<Dictionary>(dictionaryDto);
            await _service.CreateDictionary(dictionary);
            await _service.Save();
            return CreatedAtRoute(nameof(GetDictionaryById), new { dictionary.Id }, dictionary);

        }

        [HttpPut]
        [Route("", Name = "UpdateDictionary")]
        public async Task<IActionResult> UpdateDictionary([FromBody] DictionaryUpdateDto dictionaryDto)
        {

            var dictionary = _mapper.Map<Dictionary>(dictionaryDto);
            _service.UpdateDictionary(dictionary);
            await _service.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteDictionary")]
        public async Task<IActionResult> DeleteDictionary(Guid id)
        {
            var item = _service.FindDictionaryByCondition(p => p.Id == id, true).FirstOrDefault();
            if (item == null)
            {
                _logger.LogInfo($"Dictionary with Id {id} doesn't exist in the database.");
                return BadRequest();
            }
            _service.DeleteDictionary(item);
            await _service.Save();
            return Ok();
        }

        //DictionaryWord
        //api/dictionary/dictionary-word/..


        [HttpGet]
        [Route("{id}/words", Name = "GetWordsFromDictionary")]
        public async Task<IActionResult> GetWordsFromDictionary(Guid id)
        {

            var dictionary =  _service.FindDictionaryByCondition(p => p.Id == id, false).FirstOrDefault();

            if (dictionary == null)
            {
                _logger.LogInfo($"Dictionary with id: {id} doesn't exist in database");
                return NotFound();
            }

            var words = (await _service.GetWordsFromDictionary(dictionary));
            var wordsDto = _mapper.Map<IEnumerable<WordGetDto>>(words);
            return Ok(wordsDto);

        }

        [HttpPost]
        [Route("{dictionaryId}/words/{wordId}", Name = "AddWordToDictionary")]
        public async Task<IActionResult> AddWordToDictionary(Guid dictionaryId, Guid wordId)
        {
            var dictionary = _service.FindDictionaryByCondition(p=>p.Id == dictionaryId, false).FirstOrDefault();
            var word = _service.FindWordByCondition(p => p.Id == wordId, false).FirstOrDefault();

            if (word == null )
            {
                _logger.LogInfo($"Word with id: {wordId} doesn't exist in database");

                return NotFound();
            }

            if (dictionary == null)
            {
                _logger.LogInfo($"Dictionary with id: {dictionaryId} doesn't exist in database");
                return NotFound();
            }

            if (_service.IsWordInDictionary(word, dictionary))
            {
                _logger.LogInfo($"This word already exist in dictionary");

                return BadRequest();
            }

            await _service.AddWordToDictionary(word,dictionary);
            await _service.Save();
            return Ok();

        }


        [HttpDelete]
        [Route("{dictionaryId}/words/{wordId}", Name = "RemoveWordFromDictionary")]
        public async Task<IActionResult> RemoveWordFromDictionary(Guid wordId, Guid dictionaryId)
        {
            var dictionary =_service.FindDictionaryByCondition(p => p.Id == dictionaryId, false).FirstOrDefault();
            var word =_service.FindWordByCondition(p => p.Id == wordId, false).FirstOrDefault();

            if (word == null)
            {
                _logger.LogInfo($"Word with id: {wordId} doesn't exist in database");

                return NotFound();
            }

            if (dictionary == null)
            {
                _logger.LogInfo($"Dictionary with id: {dictionaryId} doesn't exist in database");
                return NotFound();
            }

            if (!_service.IsWordInDictionary(word, dictionary))
            {
                _logger.LogInfo($"Word with id: {wordId} doesn't  exist in dictionary with id: {dictionaryId}");

                return BadRequest();
            }

            _service.RemoveWordFromDictionary(word, dictionary);
            await _service.Save();
            return NoContent();
        }
    }
}

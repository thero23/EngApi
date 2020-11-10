using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using EnglishApi.Data;
using EnglishApi.Data.Interfaces;
using EnglishApi.Data.Repositories;
using EnglishApi.DTOs;
using EnglishApi.Logger;
using EnglishApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;


namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionariesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;



        public DictionariesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {

            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }


        [HttpGet]
        [Route("words")]
        public async Task<ActionResult<IEnumerable<Word>>> GetAllWords()
        {
           
            var words = await _repository.Word.FindAll(false);
            var wordsDto = _mapper.Map<IEnumerable<WordGetDto>>(words);
            return Ok(wordsDto);

            
        }

        [HttpGet]
        [Route("words/{id}", Name = "GetWordById")]
        public async Task<ActionResult<WordGetDto>> GetWordById(Guid id)
        {
            var word = (await _repository.Word.FindByCondition(p => p.Id == id, false)).FirstOrDefault();
            
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
             _repository.Word.Create(word);
            await _repository.Save();
            return CreatedAtRoute(nameof(GetWordById), new { word.Id }, word);

        }

        [HttpDelete]
        [Route("words/{id}", Name = "DeleteWord")]
        public async Task<IActionResult> DeleteWord(Guid id)
        {
            var item = (await _repository.Word.FindByCondition(p=>p.Id == id,true)).First();
            if (item == null)
            {
                return BadRequest();
            } 
            _repository.Word.Delete(item);
            await _repository.Save();
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
            _repository.Word.Update(word);
            await _repository.Save();
            return Ok();
        }


        //Dictionary
        // api/dictionaries/...


        [HttpGet]
        [Route("", Name = "GetAllDictionaries")]
        public async Task<ActionResult<IEnumerable<DictionaryGetDto>>> GetAllDictionaries()
        {
            var dictionaries =await _repository.Dictionary.FindAll(false);
            var dictionariesDto = _mapper.Map<IEnumerable<DictionaryGetDto>>(dictionaries);
            return Ok(dictionariesDto);
        }

        [HttpGet]
        [Route("{id}", Name = "GetDictionaryById")]
        public async Task<ActionResult<Dictionary>> GetDictionaryById(Guid id)
        {
            var item =await _repository.Dictionary.FindByCondition(p => p.Id == id, false);
            if (item == null)
            {
                _logger.LogInfo($"Dictionary with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            return Ok();
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
            _repository.Dictionary.Create(dictionary);
            await _repository.Save();
            return CreatedAtRoute(nameof(GetDictionaryById), new { dictionary.Id }, dictionary);

        }

        [HttpPut]
        [Route("", Name = "UpdateDictionary")]
        public async Task<IActionResult> UpdateDictionary([FromBody] DictionaryUpdateDto dictionaryDto)
        {

            var dictionary = _mapper.Map<Dictionary>(dictionaryDto);
            _repository.Dictionary.Update(dictionary);
            await _repository.Save();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteDictionary")]
        public async Task<IActionResult> DeleteDictionary(Guid id)
        {
            var item = (await _repository.Dictionary.FindByCondition(p => p.Id == id, true)).FirstOrDefault();
            if (item == null)
            {
                _logger.LogInfo($"Dictionary with Id {id} doesn't exist in the database.");
                return BadRequest();
            }
            _repository.Dictionary.Delete(item);
            await _repository.Save();
            return Ok();
        }

        //DictionaryWord
        //api/dictionary/dictionary-word/..


        [HttpGet]
        [Route("{id}/words", Name = "GetWordsFromDictionary")]
        public async Task<ActionResult<IEnumerable<Word>>> GetWordsFromDictionary(Guid id)
        {
            var dictionary = (await _repository.Dictionary.FindByCondition(p => p.Id == id, false)).FirstOrDefault();

            if (dictionary == null)
            {
                _logger.LogInfo($"Dictionary with id: {id} doesn't exist in database");
                return NotFound();
            }

            var words = (await _repository.DictionaryWord.GetWordsFromDictionary(dictionary));
            var wordsDto = _mapper.Map<IEnumerable<WordGetDto>>(words);
            return Ok(wordsDto);

        }

        [HttpPost]
        [Route("{dictionaryId}/words/{wordId}", Name = "AddWordToDictionary")]
        public async Task<IActionResult> AddWordToDictionary(Guid dictionaryId, Guid wordId)
        {
            var dictionary = (await _repository.Dictionary.FindByCondition(p=>p.Id == dictionaryId, false)).FirstOrDefault();
            var word = (await _repository.Word.FindByCondition(p => p.Id == wordId, false)).FirstOrDefault();

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

            if (_repository.DictionaryWord.IsWordInDictionary(word, dictionary))
            {
                _logger.LogInfo($"This word already exist in dictionary");

                return BadRequest();
            }

            _repository.DictionaryWord.AddWordToDictionary(word,dictionary);
            await _repository.Save();
            return Ok();

        }


        [HttpDelete]
        [Route("{dictionaryId}/words/{wordId}", Name = "RemoveWordFromDictionary")]
        public async Task<IActionResult> RemoveWordFromDictionary(Guid wordId, Guid dictionaryId)
        {
            var dictionary =(await _repository.Dictionary.FindByCondition(p => p.Id == dictionaryId, false)).FirstOrDefault();
            var word =(await _repository.Word.FindByCondition(p => p.Id == wordId, false)).FirstOrDefault();

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

            if (!_repository.DictionaryWord.IsWordInDictionary(word, dictionary))
            {
                _logger.LogInfo($"Word with id: {wordId} doesn't  exist in dictionary with id: {dictionaryId}");

                return BadRequest();
            }

            _repository.DictionaryWord.RemoveWordFromDictionary(word, dictionary);
            await _repository.Save();
            return NoContent();
        }
    }
}

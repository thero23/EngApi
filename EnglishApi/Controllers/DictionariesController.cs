using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using English.Services.Interfaces;
using EnglishApi.ModelBinders;
using Entities.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [Route("words/collection/({ids})", Name = "GetWordsByIds")]
        public IActionResult GetWordsByIds([ModelBinder(BinderType =
            typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var words = _service.FindWordsByIds(ids,false);
            if (ids.Count() != words.Result.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var wordsDto = new List<WordGetDto>();
            foreach (var word in words.Result)
            {
                wordsDto.Add(_mapper.Map<WordGetDto>(word));
            }
            //var wordsDto = _mapper.Map<IEnumerable<WordGetDto>>(words);
            return Ok(wordsDto);


        }

        [HttpGet]
        [Route("words/{id}", Name = "GetWordById")]
        public IActionResult GetWordById(Guid id)
        {
            var word = _service.FindWordByCondition(p => p.Id.Equals(id) , false).FirstOrDefault();
            
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
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the WordCreateDto object");
                return UnprocessableEntity(ModelState);
            }

            var word = _mapper.Map<Word>(wordDto);
            await _service.CreateWord(word);
            await _service.Save();
            return CreatedAtRoute(nameof(GetWordById), new { word.Id }, word);

        }

        [HttpPost]
        [Route("words/collection")]
        public async Task<IActionResult> CreateWordsCollection([FromBody] IEnumerable<WordCreateDto> wordCollection)
        {
            var wordCreateDtos = _mapper.Map<IEnumerable<Word>>(wordCollection);
            foreach (var word in wordCreateDtos)
            {
                await _service.CreateWord(word);
            }

            await _service.Save();

            var wordCollectionToReturn = _mapper.Map <IEnumerable<WordGetDto>>(wordCreateDtos);
            var ids = string.Join(",", wordCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute(nameof(GetWordsByIds), new {ids}, wordCollectionToReturn);
        }

        [HttpDelete]
        [Route("words/{id}", Name = "DeleteWord")]
        public async Task<IActionResult> DeleteWord(Guid id)
        {

            var item = _service.FindWordByCondition(p=>p.Id.Equals(id) ,true).FirstOrDefault();
            if (item == null)
            {
                _logger.LogInfo($"Word with id: {id} doesn't exist in the database.");
                return NotFound();
            } 
            _service.DeleteWord(item);
            await _service.Save();
            return NoContent();
        }

        [HttpPut]
        [Route("words", Name = "UpdateWord")]
        public async Task<IActionResult> UpdateWord([FromBody] WordUpdateDto wordDto)
        {
            if (wordDto == null)
            {
                _logger.LogInfo($"Word with id: {wordDto.Id} doesn't exist in the database.");
                return NotFound();
            }

            var word = _mapper.Map<Word>(wordDto);
            _service.UpdateWord(word);
            await _service.Save();
            return NoContent();
        }

        [HttpPatch]
        [Route("words/{id}")]
        public async Task<IActionResult> PartiallyUpdateWord(Guid id, [FromBody] JsonPatchDocument<WordUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var word = _service.FindWordByCondition(x => x.Id.Equals(id), true).FirstOrDefault();

            if (word == null)
            {
                _logger.LogInfo($"Word with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var wordToPatch = _mapper.Map<WordUpdateDto>(word);
            patchDoc.ApplyTo(wordToPatch, ModelState);
            TryValidateModel(wordToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(wordToPatch, word);

            await _service.Save();
            return NoContent();
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
            var item = _service.FindDictionaryByCondition(p => p.Id.Equals(id), false);
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
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the WordCreateDto object");
                return UnprocessableEntity(ModelState);
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

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PartiallyUpdateDictionary(Guid id, [FromBody] JsonPatchDocument<DictionaryUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var dictionary = _service.FindDictionaryByCondition(x => x.Id.Equals(id), true).FirstOrDefault();

            if (dictionary == null)
            {
                _logger.LogInfo($"Dictionary with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var dictionaryToPatch = _mapper.Map<DictionaryUpdateDto>(dictionary);
          
            patchDoc.ApplyTo(dictionaryToPatch, ModelState);
            TryValidateModel(dictionaryToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }


            _mapper.Map(dictionaryToPatch, dictionary);

            await _service.Save();
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteDictionary")]
        public async Task<IActionResult> DeleteDictionary(Guid id)
        {
            var item = _service.FindDictionaryByCondition(p => p.Id.Equals(id), true).FirstOrDefault();
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

            var dictionary =  _service.FindDictionaryByCondition(p => p.Id.Equals(id), false).FirstOrDefault();

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
            var dictionary = _service.FindDictionaryByCondition(p=>p.Id.Equals(dictionaryId) , false).FirstOrDefault();
            var word = _service.FindWordByCondition(p => p.Id.Equals(wordId) , false).FirstOrDefault();

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
            var dictionary =_service.FindDictionaryByCondition(p => p.Id.Equals(dictionaryId), false).FirstOrDefault();
            var word =_service.FindWordByCondition(p => p.Id.Equals(wordId) , false).FirstOrDefault();

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

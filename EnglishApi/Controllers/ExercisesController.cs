using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using English.Services.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseAnswerService _service;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;



        public ExercisesController(IExerciseAnswerService service, ILoggerManager logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("", Name = "GetAllExercises")]
        public async Task<IActionResult> GetAllExercises()
        {
            var exercises = await _service.GetAllExercises(false);

            return Ok(exercises);
        }

        [HttpGet]
        [Route("{id}", Name = "GetExerciseById")]
        public async Task<IActionResult> GetExerciseById(Guid id)
        {
            var exercise = await _service.GetExerciseByCondition(ex => ex.Id.Equals(id), false);

            if (exercise == null) return NotFound();

            return Ok(exercise);
        }


        [HttpPost]
        [Route("", Name = "AddExercise")]
        public async Task<IActionResult> AddExercise([FromBody] Exercise exercise)
        {
            await _service.CreateExercise(exercise);
            await _service.Save();
            return CreatedAtRoute(nameof(GetExerciseById), new { exercise.Id }, exercise);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteExercise")]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            var exercise = (await _service.GetExerciseByCondition(p => p.Id == id, true)).FirstOrDefault();
            if (exercise == null)
            {
                return NotFound();
            }

            _service.DeleteExercise(exercise);
            await _service.Save();
            return NoContent();
        }

        [HttpPut]
        [Route("", Name = "UpdateExercise")]
        public async Task<IActionResult> UpdateSubsection([FromBody] Exercise exercise)
        {
            _service.UpdateExercise(exercise);
            await _service.Save();
            return Ok(exercise);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PersonalGrowthTracker.Api.Domain.Entities;
using PersonalGrowthTracker.Api.Domain.Repositories;

namespace PersonalGrowthTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodController : ControllerBase
    {
        private readonly IMoodRepository _repository;

        public MoodController(IMoodRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public ActionResult<IEnumerable<MoodEntry>> GetAll() 
        {
            var items = _repository.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
        public ActionResult<MoodEntry> GetById(int id)
        {
            var item = _repository.GetById(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ActionResult<MoodEntry> Create([FromBody] MoodEntry request) 
        {
            if (request.Mood < 1 || request.Mood > 10)
            {
                return ValidationProblem("Mood must be between 1 and 10.");
            }

            request.Id = 0;
            request.CreatedAtUTC = DateTime.UtcNow;
            var created = _repository.Add(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}

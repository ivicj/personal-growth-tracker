using Microsoft.AspNetCore.Mvc;
using PersonalGrowthTracker.Api.Domain.Entities;
using PersonalGrowthTracker.Api.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace PersonalGrowthTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodController : ControllerBase
    {
        private readonly IMoodRepository _repository;
        private readonly ILogger<MoodController> _logger;

        public MoodController(IMoodRepository repository, ILogger<MoodController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByHeader = "Accept")]
        public ActionResult<IEnumerable<MoodEntry>> GetAll()
        {
            _logger.LogInformation("GET /api/mood called at {TimeUtc}", DateTime.UtcNow);

            var items = _repository.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByHeader = "Accept")]
        public ActionResult<MoodEntry> GetById(int id)
        {
            _logger.LogInformation("GET /api/mood/{Id} called at {TimeUtc}", id, DateTime.UtcNow);

            var item = _repository.GetById(id);
            if (item is null)
            {
                _logger.LogWarning("Mood entry with id {Id} not found", id);
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
                _logger.LogWarning("Invalid mood value {Mood} received", request.Mood);
                return ValidationProblem("Mood must be between 1 and 10.");
            }
            _logger.LogInformation("Creating new mood entry with mood {Mood} at {TimeUtc}", request.Mood, DateTime.UtcNow);

            request.Id = 0;
            request.CreatedAtUTC = DateTime.UtcNow;
            var created = _repository.Add(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}

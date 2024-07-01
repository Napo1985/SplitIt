using Microsoft.AspNetCore.Mvc;
using Splitit.Splitit.Entities;
using Splitit.Splitit.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Splitit.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ActorService _actorService;
        private readonly IMapper _mapper;

        public ActorsController(ActorService actorService, IMapper mapper)
        {
            _actorService = actorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors([FromQuery] string name, [FromQuery] int? minRank, [FromQuery] int? maxRank, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var actors = await _actorService.GetAllActorsAsync();
            // Filter and paginate logic
            if (!string.IsNullOrEmpty(name))
            {
                actors = actors.Where(a => a.Name.Contains(name));
            }
            if (minRank.HasValue)
            {
                actors = actors.Where(a => a.Rank.Value >= minRank.Value);
            }
            if (maxRank.HasValue)
            {
                actors = actors.Where(a => a.Rank.Value <= maxRank.Value);
            }
            var paginatedActors = actors.Skip((page - 1) * pageSize).Take(pageSize);
            var actorDtos = _mapper.Map<IEnumerable<ActorDto>>(paginatedActors);
            return Ok(actorDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> GetActor(int id)
        {
            var actor = await _actorService.GetActorByIdAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            var actorDto = _mapper.Map<ActorDto>(actor);
            return Ok(actorDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddActor([FromBody] ActorDto actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);
            await _actorService.AddActorAsync(actor);
            return CreatedAtAction(nameof(GetActor), new { id = actor.Id }, actorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateActor(int id, [FromBody] ActorDto actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);
            actor.Id = id;
            await _actorService.UpdateActorAsync(actor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActor(int id)
        {
            await _actorService.DeleteActorAsync(id);
            return NoContent();
        }
    }
}


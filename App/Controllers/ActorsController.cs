using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Splitit.App.Models;
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

        public ActorsController(ActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Actor>> GetAllActors()
        {
            var actors = _actorService.GetAllActors();
            return Ok(actors);
        }

        [HttpGet("{id}")]
        public ActionResult<Actor> GetActorById(int id)
        {
            var actor = _actorService.GetActorById(id);
            if (actor == null)
            {
                return NotFound();
            }
            return Ok(actor);
        }

        [HttpPost]
        public ActionResult AddActor([FromBody] Actor actor)
        {
            _actorService.AddActor(actor);
            return CreatedAtAction(nameof(GetActorById), new { id = actor.Id }, actor);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateActor(int id, [FromBody] Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }
            _actorService.UpdateActor(actor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteActor(int id)
        {
            _actorService.DeleteActor(id);
            return NoContent();
        }

        [HttpGet("imdb")]
        public ActionResult<IEnumerable<Actor>> GetActorsFromImdb()
        {
            var actors = _actorService.GetActorsFromImdb();
            return Ok(actors);
        }
    }
}


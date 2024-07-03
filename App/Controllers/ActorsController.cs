using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Splitit.App.Exceptions;
using Splitit.App.Models;
using Splitit.Splitit.Dto;
using Splitit.Splitit.Entities;
using Splitit.Splitit.Services;
using Splitit.Splitit.ValueObjects;

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
        public ActionResult<IEnumerable<Actor>> GetAllActors([FromQuery] ActorSearchCriteria criteria)
        {
            var actors = _actorService.GetAllActors(criteria);
            return Ok(actors);
        }

        [HttpGet("{id}")]
        public ActionResult<Actor> GetActorById(string id)
        {
            var actor = _actorService.GetActorById(id);
            if (actor == null)
            {
                return NotFound();
            }
            return Ok(actor);
        }

        [HttpPost]
        public ActionResult AddActor([FromBody] ActorRequest actor)
        {
            try
            {
                string actorId = _actorService.AddActor(new DetailedActorDto(actor.Id, actor.Name, actor.Details, actor.Type, new Rank(actor.Rank), actor.Source)) ;
                return Ok($"Id = {actorId}");
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationAppException(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public ActionResult UpdateActor(string id, [FromBody] ActorRequest actor)
        {
            try
            {
                _actorService.UpdateActor(id ,new DetailedActorDto(actor.Id, actor.Name, actor.Details, actor.Type, new Rank(actor.Rank), actor.Source));
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationAppException(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteActor(string id)
        {
            _actorService.DeleteActor(id);
            return Ok();
        }

        [HttpGet("imdb")]
        public ActionResult<IEnumerable<Actor>> GetActorsFromImdb()
        {
            var actors = _actorService.GetActorsFromImdb();
            return Ok(actors);
        }
    }

}
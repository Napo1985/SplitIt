using System;
using Splitit.Infra.Providers;
using Splitit.Splitit.Entities;
using Splitit.Splitit.Repositories;

namespace Splitit.Splitit.Services
{
    public class ActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IActorProvider _actorProvider;

        public ActorService(IActorRepository actorRepository, IActorProvider actorProvider)
        {
            _actorRepository = actorRepository;
            _actorProvider = actorProvider;
        }

        public IEnumerable<Actor> GetAllActors()
        {
            return _actorRepository.GetAll();
        }

        public Actor GetActorById(int id)
        {
            return _actorRepository.GetById(id);
        }

        public void AddActor(Actor actor)
        {
            _actorRepository.Add(actor);
        }

        public void UpdateActor(Actor actor)
        {
            _actorRepository.Update(actor);
        }

        public void DeleteActor(int id)
        {
            _actorRepository.Delete(id);
        }

        public IEnumerable<Actor> GetActorsFromImdb()
        {
            var actors = _actorProvider.GetActorsAsync().Result;
            return actors;
        }
    }
}


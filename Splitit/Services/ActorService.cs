using System;
using Splitit.Splitit.Entities;
using Splitit.Splitit.Repositories;

namespace Splitit.Splitit.Services
{
    public class ActorService
    {
        private readonly IActorRepository _actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<IEnumerable<Actor>> GetAllActorsAsync()
        {
            return await _actorRepository.GetAllAsync();
        }

        public async Task<Actor> GetActorByIdAsync(int id)
        {
            return await _actorRepository.GetByIdAsync(id);
        }

        public async Task AddActorAsync(Actor actor)
        {
            await _actorRepository.AddAsync(actor);
        }

        public async Task UpdateActorAsync(Actor actor)
        {
            await _actorRepository.UpdateAsync(actor);
        }

        public async Task DeleteActorAsync(int id)
        {
            await _actorRepository.DeleteAsync(id);
        }
    }
}


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
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public ActorService(IActorRepository actorRepository, IActorProvider actorProvider)
        {
            _actorRepository = actorRepository;
            _actorProvider = actorProvider;
        }

        public IEnumerable<Actor> GetAllActors(ActorSearchCriteria criteria)
        {
            _lock.EnterReadLock();
            try
            {
                var actors = _actorRepository.GetAll()
                    .Where(a => (criteria.ActorName == null || a.Name.Contains(criteria.ActorName)) &&
                                (criteria.MinRank == null || a.Rank >= criteria.MinRank) &&
                                (criteria.MaxRank == null || a.Rank <= criteria.MaxRank) &&
                                (criteria.Provider == null || a.Source == criteria.Provider))
                    .Skip(criteria.Skip)
                    .Take(criteria.Take);

                return actors;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public Actor GetActorById(string id)
        {
            _lock.EnterReadLock();
            try
            {
                return _actorRepository.GetById(id);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void AddActor(Actor actor)
        {
            _lock.EnterWriteLock();
            try
            {
                _actorRepository.Add(actor);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void UpdateActor(Actor actor)
        {
            _lock.EnterWriteLock();
            try
            {
                _actorRepository.Update(actor);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void DeleteActor(string id)
        {
            _lock.EnterWriteLock();
            try
            {
                _actorRepository.Delete(id);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public IEnumerable<Actor> GetActorsFromImdb()
        {
            return _actorProvider.GetActorsAsync().Result;
        }
    }
}


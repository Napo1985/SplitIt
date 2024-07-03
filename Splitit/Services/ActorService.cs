using System;
using Splitit.Infra.Providers;
using Splitit.Splitit.Dto;
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
            var actors = _actorRepository.GetAll()
              .Where(a => (criteria.ActorName == null || a.Name.Contains(criteria.ActorName)) &&
                         (criteria.MinRank == null || a.Rank.Value >= criteria.MinRank) &&
                         (criteria.MaxRank == null || a.Rank.Value <= criteria.MaxRank))
             .Skip(criteria.Skip)
             .Take(criteria.Take);

            return actors;

        }

        public Actor GetActorById(string id)
        {
            try
            {
                _lock.EnterReadLock();
                return _actorRepository.GetById(id);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public string AddActor(DetailedActorDto actorDto)
        {
            _lock.EnterWriteLock();
            try
            {
                var existingActor = _actorRepository.GetById(actorDto.Id);
                if (existingActor == null)
                {
                    CheckDuplication(actorDto);
                    var actor = new Actor(actorDto.Name, actorDto.Details, actorDto.Type, actorDto.Rank, actorDto.Source);

                    return _actorRepository.Add(actor);
                }
                else
                {
                    throw new InvalidOperationException("An actor with the same ID already exists.");
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }



        public void UpdateActor(string id, DetailedActorDto actorDto)
        {
            _lock.EnterWriteLock();
            try
            {
                var existingActor = _actorRepository.GetById(id);
                if (existingActor != null)
                {
                    CheckDuplication(actorDto);
                    var actor = new Actor(actorDto.Name, actorDto.Details, actorDto.Type, actorDto.Rank, actorDto.Source, id);
                    _actorRepository.Update(actor);
                }
                else
                {
                    throw new InvalidOperationException("An actor with the same ID not exists.");
                }

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

        private void CheckDuplication(DetailedActorDto actorDto)
        {
            var allActors = _actorRepository.GetAll();
            var isRankDuplicated = allActors.Any(a => a.Id != actorDto.Id && a.Rank.Value == actorDto.Rank.Value);

            if (isRankDuplicated)
            {
                throw new InvalidOperationException($"An actor with the rank {actorDto.Rank.Value} already exists.");
            }
        }
    }
}


using System;
using Splitit.Splitit.Entities;

namespace Splitit.Splitit.Repositories
{
    public interface IActorRepository
    {
        IEnumerable<Actor> GetAll();
        Actor GetById(int id);
        void Add(Actor actor);
        void Update(Actor actor);
        void Delete(int id);
    }
}


using System;
using Splitit.Splitit.Entities;

namespace Splitit.Splitit.Repositories
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor> GetByIdAsync(int id);
        Task AddAsync(Actor actor);
        Task UpdateAsync(Actor actor);
        Task DeleteAsync(int id);
    }
}


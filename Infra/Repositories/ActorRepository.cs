using System;
using Microsoft.EntityFrameworkCore;
using Splitit.Infra.Data;
using Splitit.Splitit.Entities;
using Splitit.Splitit.Repositories;

namespace Splitit.Infra.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly ActorContext _context;

        public ActorRepository(ActorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await _context.Actors.ToListAsync();
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            return await _context.Actors.FindAsync(id);
        }

        public async Task AddAsync(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Actor actor)
        {
            _context.Actors.Update(actor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
            }
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Splitit.Splitit.Entities;

namespace Splitit.Infra.Data
{
    public class ActorContext : DbContext
    {
        public ActorContext(DbContextOptions<ActorContext> options) : base(options) { }

        public DbSet<Actor> Actors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Actor>().HasKey(a => a.Id);
            modelBuilder.Entity<Actor>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Actor>().OwnsOne(a => a.Rank);
        }
    }
}


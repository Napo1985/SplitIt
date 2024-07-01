using System;
namespace Splitit.Splitit.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Rank Rank { get; set; }

        public Actor(int id, string name, Rank rank)
        {
            Id = id;
            Name = name;
            Rank = rank;
        }
    }
}


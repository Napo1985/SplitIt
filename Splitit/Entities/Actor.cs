using System;
using System.ComponentModel.DataAnnotations;
using Splitit.Splitit.ValueObjects;

namespace Splitit.Splitit.Entities
{
    public class Actor
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Details { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        public Rank Rank { get; set; }

        [StringLength(100)]
        public string Source { get; set; }
    }
}


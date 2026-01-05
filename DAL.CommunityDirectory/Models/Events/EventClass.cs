using DAL.CommunityDirectory.Models.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CommunityDirectory.Models.Event
{
    public class EventClass : BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Organizer { get; set; }

        public string? ImagePath { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual CategoryClass? Category { get; set; }
    }
}

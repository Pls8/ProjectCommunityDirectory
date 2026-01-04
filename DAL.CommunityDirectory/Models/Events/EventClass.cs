using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string Organizer { get; set; }

        public string ImagePath { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.CommunityDirectory.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string? Organizer { get; set; }
        public string? ImagePath { get; set; }
        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }

        //updated for DTO
        //public int Id { get; set; }
        //public string Title { get; set; }
        //public string Description { get; set; }
        //public DateTime EventDate { get; set; }
        //public string Location { get; set; }
        //public int CategoryId { get; set; } 
        //public string? CategoryName { get; set; }
    }
}

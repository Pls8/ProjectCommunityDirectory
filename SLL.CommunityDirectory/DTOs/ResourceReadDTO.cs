using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.CommunityDirectory.DTOs
{
    public class ResourceReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; }
        public string? Phone { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactInfo { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public bool IsApproved { get; set; } // So the UI knows the status
        public int CategoryId { get; set; }
    }
}

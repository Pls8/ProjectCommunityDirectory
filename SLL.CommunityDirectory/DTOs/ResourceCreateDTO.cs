using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.CommunityDirectory.DTOs
{
    public class ResourceCreateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? Phone { get; set; }
        [EmailAddress]
        public string? ContactEmail { get; set; }
        public string? ContactInfo { get; set; }
        [Required]
        public string City { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public bool IsApproved { get; set; }
    }
}

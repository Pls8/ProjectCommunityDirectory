using DAL.CommunityDirectory.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CommunityDirectory.Models.Resources
{
    public class ResourceClass : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string? Description { get; set; }
        

        [EmailAddress]
        public string? ContactEmail { get; set; }
        [Phone]
        public string? Phone { get; set; }

        public string? ContactInfo { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Website { get; set; }
        public bool IsApproved { get; set; } = false; // Admin approval requirement


        [ForeignKey(nameof(category))]
        public int CategoryId { get; set; }
        public virtual CategoryClass? category { get; set; }


        [ForeignKey(nameof(SubmittedBy))]
        public string? SubmittedById { get; set; }
        public virtual AppUser? SubmittedBy { get; set; }
    }
}

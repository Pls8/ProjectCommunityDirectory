using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CommunityDirectory.Models.Resources
{
    public class CategoryClass : BaseEntity
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        // Navigation property for Resources
        public virtual ICollection<ResourceClass>? Resources { get; set; }
        public virtual ICollection<Event.EventClass>? Events { get; set; }
    }
}

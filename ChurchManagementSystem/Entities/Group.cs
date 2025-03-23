using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Group name is required.")]
        [StringLength(100, ErrorMessage = "Group name cannot exceed 100 characters.")]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Display(Name = "Group Description")]
        public string Description { get; set; }
    }
}

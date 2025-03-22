using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Member
    {

        public Guid Id { get; set; } 
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LName { get; set; }

        [Range(0, 150, ErrorMessage = "Age must be between 0 and 150")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Sex is required")]
        [EnumDataType(typeof(SexType), ErrorMessage = "Invalid value for Sex")]
        public SexType Sex { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters")]
        public string City { get; set; }

        [StringLength(100, ErrorMessage = "State name cannot exceed 100 characters")]
        public string State { get; set; }

        [StringLength(10, ErrorMessage = "Zipcode cannot exceed 10 characters")]
        public string Zipcode { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; } = DateTime.Now;


        public string? PicturePath { get; set; }

        public bool? PriorMembership { get; set; } = false;

        [DataType(DataType.DateTime)]
        [Display(Name = "Baptismal Date")]
        public DateTime BaptismalDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

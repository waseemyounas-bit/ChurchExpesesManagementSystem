using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BankAccount
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Account Number is required")]
        [RegularExpression(@"^\d{6,20}$", ErrorMessage = "Account Number must be between 6 to 20 digits")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Routing Number is required")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Routing Number must be exactly 9 digits")]
        public string RoutingNumber { get; set; }

        [Required(ErrorMessage = "Bank Name is required")]
        [StringLength(100, ErrorMessage = "Bank Name cannot exceed 100 characters")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(20, ErrorMessage = "State cannot exceed 20 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "ZIP Code is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid ZIP code format")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Contact name is required")]
        [StringLength(100, ErrorMessage = "Contact name cannot exceed 100 characters")]
        public string Contact { get; set; }


    }

}

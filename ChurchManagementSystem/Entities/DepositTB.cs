using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DepositTB
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Bank is required")]
        public string Bank { get; set; }

        [Required(ErrorMessage = "Account Number is required")]
        public string AccountNumber { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cheque amount must be a non-negative number")]
        [Required(ErrorMessage ="Please enter cheque amount.")]
        public double Cheque { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cash must be a non-negative number")]
        public double Cash { get; set; }

        [StringLength(5, MinimumLength = 5, ErrorMessage = "Check must be exactly 5 characters")]
        public string Check { get; set; }

        //[StringLength(5, MinimumLength = 5, ErrorMessage = "Check2 must be exactly 5 characters")]
        //public string? Check2 { get; set; }

        //[StringLength(5, MinimumLength = 5, ErrorMessage = "Check3 must be exactly 5 characters")]
        //public string? Check3 { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
    }
}

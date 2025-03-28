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

        [Range(0, double.MaxValue, ErrorMessage = "Cheque must be a non-negative number")]
        public double Cheque { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cash must be a non-negative number")]
        public double Cash { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
    }
}

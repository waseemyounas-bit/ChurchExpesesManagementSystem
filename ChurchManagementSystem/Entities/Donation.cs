using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Donation
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Member is required.")]
        public Guid? MemberId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Donation type is required.")]
        [StringLength(100, ErrorMessage = "Donation type cannot exceed 100 characters.")]
        public string Type { get; set; }


        public DateTime Date { get; set; } = DateTime.Now;

        public Member? Member { get; set; }
    }
}

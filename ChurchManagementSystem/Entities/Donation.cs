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


        public Guid? MemberId { get; set; }

        public Guid? VisitorId { get; set; }

       
        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Donation type is required.")]
        [StringLength(100, ErrorMessage = "Donation type cannot exceed 100 characters.")]
        public string Type { get; set; }

        public DateTime DueDate { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public Member? Member { get; set; }
        public Visitor? Visitor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MemberId == null && VisitorId == null)
            {
                yield return new ValidationResult("Either Member or Visitor must be selected.", new[] { "MemberId", "VisitorId" });
            }

            if (MemberId != null && VisitorId != null)
            {
                yield return new ValidationResult("Select only one: Member or Visitor — not both.", new[] { "MemberId", "VisitorId" });
            }
        }
    }
}

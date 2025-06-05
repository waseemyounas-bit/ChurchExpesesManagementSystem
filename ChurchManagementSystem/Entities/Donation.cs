using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Donation
    {
        public Guid Id { get; set; }
        // This property will be bound from the form to determine donor type
        [Display(Name = "Is Member Donation")]
        [NotMapped]
        public bool IsMemberDonation { get; set; } = true;

        [RequiredIf(nameof(IsMemberDonation), true, ErrorMessage = "Please select a member.")]
        public Guid? MemberId { get; set; }

        [RequiredIf(nameof(IsMemberDonation), false, ErrorMessage = "Please select a visitor.")]
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

public class RequiredIfAttribute : ValidationAttribute
    {
        private readonly string _dependentProperty;
        private readonly object _targetValue;

        public RequiredIfAttribute(string dependentProperty, object targetValue)
        {
            _dependentProperty = dependentProperty;
            _targetValue = targetValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the dependent property
            PropertyInfo? property = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (property == null)
                return new ValidationResult($"Unknown property: {_dependentProperty}");

            // Get the value of the dependent property
            object? dependentValue = property.GetValue(validationContext.ObjectInstance, null);

            // Check if the condition matches
            if ((dependentValue == null && _targetValue == null) ||
                (dependentValue != null && dependentValue.Equals(_targetValue)))
            {
                // Validate the current property value
                if (value == null || (value is Guid guid && guid == Guid.Empty))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }

}

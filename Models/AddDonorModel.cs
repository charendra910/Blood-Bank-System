using System.ComponentModel.DataAnnotations;

namespace Blood_Bank_System.Models
{
    public class AddDonorModel
    {
        [Key]
        public int DonorId { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name cannot be longer than 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18, 60, ErrorMessage = "Age must be between 18 and 65")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Blood Type is required")]
        [RegularExpression("^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid Blood Type")]
        public string BloodType { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid Contact Number")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(50, ErrorMessage = "State cannot be longer than 50 characters")]
        public string State { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last Donation Date")]
        [CustomValidation(typeof(AddDonorModel), nameof(ValidateDonationDate))]
        public DateTime? LastDonationDate { get; set; }

        // Custom validation method for Last Donation Date
        public static ValidationResult ValidateDonationDate(DateTime? lastDonationDate, ValidationContext context)
        {
            if (lastDonationDate.HasValue && lastDonationDate.Value > DateTime.Today)
            {
                return new ValidationResult("Last Donation Date cannot be in the future");
            }
            return ValidationResult.Success;
        }

    }
}

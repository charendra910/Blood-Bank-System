using System.ComponentModel.DataAnnotations;

namespace Blood_Bank_System.Models
{
    public class ReceiveBloodModel
    {
        [Key]
        public int ReceiverID { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Blood Type is required")]
        [RegularExpression("^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid blood type format")]
        public string BloodType { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid contact number format")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Date of Requirement is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Requirement")]
        public DateTime DateOfRequirement { get; set; }

        [Required(ErrorMessage = "Enter the reason for blood requirement")]
        [StringLength(500, ErrorMessage = "Medical History cannot exceed 500 characters")]
        [Display(Name = "Medical History / Reason for Requirement")]
        public string MedicalHistory { get; set; }
    }
}


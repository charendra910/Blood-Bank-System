using System.ComponentModel.DataAnnotations;

namespace Blood_Bank_System.Models
{
    public class BloodStockModel
    {
        [Key]
        public int Id { get; set; } // Unique ID for each stock entry

        [Required]
        [Display(Name = "Blood Type")]
        [RegularExpression(@"^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid blood type. Valid types are A+, A-, B+, B-, AB+, AB-, O+, O-.")]
        public string BloodType { get; set; } // E.g., A+, O-, B+, etc.

        [Required]
        [Display(Name = "Quantity Available")]
        public int QuantityAvailable { get; set; } // The number of units available
    }
}

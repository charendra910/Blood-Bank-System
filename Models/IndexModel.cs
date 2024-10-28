using System.ComponentModel.DataAnnotations;

namespace Blood_Bank_System.Models
{
    public class IndexModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phonenumber is required")]
        public string PhoneNumber { get; set; }
    }
}

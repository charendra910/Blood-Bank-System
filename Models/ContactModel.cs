using System.ComponentModel.DataAnnotations;

namespace Blood_Bank_System.Models
{
    public class ContactModel
    {
        [Key]

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address should be provided")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Message should be filled")]
        public string Message { get; set; }

    }
}

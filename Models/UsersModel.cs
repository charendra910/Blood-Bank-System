
using Microsoft.AspNetCore.Identity;

namespace Blood_Bank_System.Models
{
    public class UsersModel : IdentityUser

    {
        public string FullName { get; set; }

    }
}

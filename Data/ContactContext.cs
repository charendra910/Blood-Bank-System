using Blood_Bank_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Blood_Bank_System.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> s) : base(s)
        {

        }

        public DbSet<ContactModel> Contactregister { get; set; }
    }
}


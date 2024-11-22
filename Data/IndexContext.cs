using Blood_Bank_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Blood_Bank_System.Data
{
    public class IndexContext : DbContext
    
    {
        public IndexContext(DbContextOptions<IndexContext> appoint) : base(appoint)
        {

        }

        public DbSet<IndexModel> AppointmentRegister { get; set; }

    }
}


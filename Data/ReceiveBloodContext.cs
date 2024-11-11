using Blood_Bank_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Blood_Bank_System.Data
{
    public class ReceiveBloodContext : DbContext
    {
        public ReceiveBloodContext(DbContextOptions<ReceiveBloodContext> s1) : base(s1)
        {

        }

        public DbSet<ReceiveBloodModel> BloodReceivers { get; set; }
    }
}

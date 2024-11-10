using Blood_Bank_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Blood_Bank_System.Data
{
    public class AddDonorContext : DbContext
    {
        public AddDonorContext(DbContextOptions<AddDonorContext> add) : base(add)
        {

        }

        public DbSet<AddDonorModel> BloodDonors { get; set; }

    }
}

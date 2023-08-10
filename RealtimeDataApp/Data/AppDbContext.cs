using Microsoft.EntityFrameworkCore;

namespace RealtimeDataApp.Data
{
    public class AppDbContext:DbContext
    {
        string _connectioString = "Server=DESKTOP-N02OMRJ\\SQLEXPRESS;Database=CompanyDatabase;Trusted_Connection=True;";

        public DbSet<Employee> Employee { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectioString);
        }
    }
}

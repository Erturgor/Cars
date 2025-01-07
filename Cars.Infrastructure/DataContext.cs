using Cars.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cars.Infrastructure
{
    public class DataContext : IdentityDbContext<AppUser?>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        // Cars to nazwa tabeli w bazie danych
        public DbSet<Car> Cars { get; set; }
    }
}
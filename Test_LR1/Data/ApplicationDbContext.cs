namespace Test_LR1.Data
{
    using Microsoft.EntityFrameworkCore;
    using Test_LR1.Models; // Укажите ваш namespace

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
    }

}

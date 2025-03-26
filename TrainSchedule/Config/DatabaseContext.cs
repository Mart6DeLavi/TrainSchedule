using Microsoft.EntityFrameworkCore;
using TrainSchedule.Entity;

namespace TrainSchedule.Config;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TrainRoute> TrainRoutes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Train_Schedule_Users;Username=postgres;Password=28859013");
    }
}
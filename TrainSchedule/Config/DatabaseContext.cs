using Microsoft.EntityFrameworkCore;
using TrainSchedule.Entity;

namespace TrainSchedule.Config;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Train_Schedule_UsersUsername=postgres;Password=123456");
    }
}
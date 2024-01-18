namespace TaskForge.Models;
using Microsoft.EntityFrameworkCore;

public class TaskForgeDbContext : DbContext
{
    public DbSet<Task> Tasks { get; set;}
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<User> Users { get; set; }

    public TaskForgeDbContext(DbContextOptions<TaskForgeDbContext> context) : base(context) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User { Id = 1, Name = "Maddi", Email = "email@email.com", FirebaseUid = "123" }
        });

        modelBuilder.Entity<Task>().HasData(new Task[]
        {
            new Task { Id = 1, Title = "Applications", Description = "Apply to 5 jobs", DueDate = DateTime.Now, UserId = 1, Status = false, },
        });

        modelBuilder.Entity<Priority>().HasData(new Priority[]
        {
            new Priority { Id = 1, Name = "High-Priority"},
            new Priority { Id = 2, Name = "Medium-Priority"},
            new Priority { Id = 3, Name = "Low-Priority"},
            new Priority { Id = 4, Name = "No Priority"}
        });
    }

}


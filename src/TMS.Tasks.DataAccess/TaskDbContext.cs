using Microsoft.EntityFrameworkCore;
using TMS.Tasks.Core.Models;

namespace TMS.Tasks.DataAccess;

public class TaskDbContext : DbContext 
{
    public DbSet<Note> Notes { get; set; }

    public TaskDbContext(DbContextOptions<TaskDbContext> options) 
        : base(options)
    {      
        Database.EnsureCreatedAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Note>()
            .Property(e => e.CreatedDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

        modelBuilder.Entity<Note>()
            .Property(e => e.UpdatedDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );
    }
}

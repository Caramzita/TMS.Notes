using Microsoft.EntityFrameworkCore;
using TMS.Notes.Core;
using TMS.Notes.DataAccess.Configurations;

namespace TMS.Notes.DataAccess;

public class NoteDbContext : DbContext
{
    public DbSet<Note> Notes { get; set; }

    public NoteDbContext(DbContextOptions<NoteDbContext> options)
        : base(options)
    {
        Database.EnsureCreatedAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new NoteConfiguration());
        base.OnModelCreating(builder);
    }
}

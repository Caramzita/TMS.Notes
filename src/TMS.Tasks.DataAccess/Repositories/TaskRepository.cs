using Microsoft.EntityFrameworkCore;
using TMS.Tasks.Core.Models;
using TMS.Tasks.UseCases.Abstractions;

namespace TMS.Tasks.DataAccess.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext сontext)
    {
        _context = сontext;
    }

    public IAsyncEnumerable<Note> GetNotes()
    {
        return _context.Notes.AsNoTracking()
                             .AsAsyncEnumerable();
    }

    public async Task<Note?> GetNoteById(Guid id)
    {
        return await _context.Notes.AsNoTracking()
                                   .FirstOrDefaultAsync(n => n.Id == id)
                                   .ConfigureAwait(false);
    }

    public async Task<Guid> Add(Note note)
    {
        await _context.AddAsync(note).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return note.Id;
    }

    public async Task Delete(Note note)
    {
        _context.Notes.Remove(note);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task Update(Note note)
    {
        _context.Notes.Update(note);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}

using Microsoft.EntityFrameworkCore;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.DataAccess.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly NoteDbContext _context;

    public NoteRepository(NoteDbContext сontext) => 
        _context = сontext;

    public IAsyncEnumerable<Note> GetNotes(Guid userId)
    {
        return _context.Notes.Where(note => note.UserId == userId)
                             .AsNoTracking()
                             .AsNoTracking()
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

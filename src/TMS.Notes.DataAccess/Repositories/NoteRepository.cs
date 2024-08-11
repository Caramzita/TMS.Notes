using Microsoft.EntityFrameworkCore;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.DataAccess.Repositories;

/// <summary>
/// Репозиторий заметок.
/// </summary>
public class NoteRepository : INoteRepository
{
    /// <summary>
    /// Контекст базы данных заметок.
    /// </summary>
    private readonly NoteDbContext _context;

    public NoteRepository(NoteDbContext context) => 
        _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// Получить все заметки пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Асинхронный список заметок.</returns>
    public IAsyncEnumerable<Note> GetNotes(Guid userId)
    {
        return _context.Notes.AsNoTracking()
                             .Where(note => note.UserId == userId)
                             .AsAsyncEnumerable();
    }

    /// <summary>
    /// Получить заметку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заметки.</param>
    /// <returns>Запрашиваемая заметка.</returns>
    public async Task<Note?> GetNoteById(Guid id)
    {
        return await _context.Notes.AsNoTracking()
                                   .FirstOrDefaultAsync(n => n.Id == id)
                                   .ConfigureAwait(false);
    }

    /// <summary>
    /// Добавить заметку.
    /// </summary>
    /// <param name="note">Заметка.</param>
    public async Task Add(Note note)
    {
        await _context.AddAsync(note).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Удалить заметку.
    /// </summary>
    /// <param name="note">Заметка.</param>
    public async Task Delete(Note note)
    {
        _context.Notes.Remove(note);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Обновить заметку.
    /// </summary>
    /// <param name="note">Заметка.</param>
    public async Task Update(Note note)
    {
        _context.Notes.Update(note);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}

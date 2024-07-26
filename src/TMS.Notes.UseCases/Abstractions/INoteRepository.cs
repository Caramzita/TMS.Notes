using TMS.Notes.Core;

namespace TMS.Notes.UseCases.Abstractions;

public interface INoteRepository
{
    Task<Guid> Add(Note note);

    Task Delete(Note note);

    Task<Note?> GetNoteById(Guid id);

    IAsyncEnumerable<Note> GetNotes();

    Task Update(Note note);
}
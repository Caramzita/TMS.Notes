using TMS.Tasks.Core.Models;

namespace TMS.Tasks.UseCases.Abstractions;

public interface ITaskRepository
{
    Task<Guid> Add(Note note);

    Task Delete(Note note);

    Task<Note?> GetNoteById(Guid id);

    IAsyncEnumerable<Note> GetNotes();

    Task Update(Note note);
}
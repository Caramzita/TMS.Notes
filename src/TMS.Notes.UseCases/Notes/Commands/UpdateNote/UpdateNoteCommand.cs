using MediatR;
using TMS.Notes.Core;

namespace TMS.Notes.UseCases.Notes.Commands.UpdateNote;

/// <summary>
/// Команда обновления заметки.
/// </summary>
public class UpdateNoteCommand : IRequest<Note>
{
    /// <summary>
    /// Идентификатор заметки.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Модель заметки.
    /// </summary>
    public NoteInputModel Model { get; }

    public UpdateNoteCommand(Guid id, NoteInputModel model)
    {
        Id = id;
        Model = model;
    }
}

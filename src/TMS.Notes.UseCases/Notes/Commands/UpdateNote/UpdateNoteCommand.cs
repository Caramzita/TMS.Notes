using MediatR;
using TMS.Application.UseCases;
using TMS.Notes.Core;

namespace TMS.Notes.UseCases.Notes.Commands.UpdateNote;

/// <summary>
/// Команда обновления заметки.
/// </summary>
public class UpdateNoteCommand : IRequest<Result<Note>>
{
    /// <summary>
    /// Идентификатор заметки.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Модель заметки.
    /// </summary>
    public NoteInputModel Model { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DeleteNoteCommand"/>.
    /// </summary>
    /// <param name="id"> Идентификатор заметки. </param>
    /// <param name="model"> Модель заметки. </param>
    public UpdateNoteCommand(Guid id, NoteInputModel model)
    {
        Id = id;
        Model = model;
    }
}

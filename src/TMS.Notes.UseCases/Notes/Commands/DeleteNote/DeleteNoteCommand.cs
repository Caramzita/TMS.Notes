using MediatR;
using TMS.Application.UseCases;

namespace TMS.Notes.UseCases.Notes.Commands.DeleteNote;

/// <summary>
/// Команда удаления заметки.
/// </summary>
public class DeleteNoteCommand : IRequest<Result<Unit>>
{
    /// <summary>
    /// Идентификатор заметки.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DeleteNoteCommand"/>.
    /// </summary>
    /// <param name="id"> Идентификатор заметки. </param>
    /// <param name="userId"> Идентификатор пользователя. </param>
    public DeleteNoteCommand(Guid id, Guid userId)
    {
        UserId = userId;
        Id = id;
    }
}

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

    public DeleteNoteCommand(Guid id, Guid userId)
    {
        UserId = userId;
        Id = id;
    }
}

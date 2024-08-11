using MediatR;
using TMS.Notes.Core;

namespace TMS.Notes.UseCases.Notes.Queries.GetNotes;

/// <summary>
/// Запрос получения всех заметок пользователя.
/// </summary>
public sealed class GetNotesQuery : IStreamRequest<Note>
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; }

    public GetNotesQuery(Guid userId)
    {
        UserId = userId;
    }
}

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

    /// <summary>
    /// Запрос для фильтрации заметок.
    /// </summary>
    public string? SearchTerm { get; }

    /// <summary>
    /// Критерий сортировки для заметок.
    /// </summary>
    public string? SortBy { get; }

    public GetNotesQuery(Guid userId, string? searchTerm = null, string? sortBy = null)
    {
        UserId = userId;
        SearchTerm = searchTerm;
        SortBy = sortBy;
    }
}

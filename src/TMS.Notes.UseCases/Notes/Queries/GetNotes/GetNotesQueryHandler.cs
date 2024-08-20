using MediatR;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Queries.GetNotes;

/// <summary>
/// Обработчик запроса получения всех заметок пользователя.
/// </summary>
public class GetNotesQueryHandler : IStreamRequestHandler<GetNotesQuery, Note>
{
    private readonly INoteRepository _noteRepository;

    public GetNotesQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    }

    public IAsyncEnumerable<Note> Handle(GetNotesQuery query, CancellationToken cancellationToken)
    {
        return _noteRepository.GetNotesAsync(query.UserId, query.SearchTerm, query.SortBy);
    }
}

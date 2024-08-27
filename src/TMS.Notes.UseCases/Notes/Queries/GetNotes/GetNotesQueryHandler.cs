using MediatR;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Queries.GetNotes;

/// <summary>
/// Обработчик запроса получения всех заметок пользователя.
/// </summary>
public class GetNotesQueryHandler : IStreamRequestHandler<GetNotesQuery, Note>
{
    /// <summary>
    /// Репозиторий заметок.
    /// </summary>
    private readonly INoteRepository _noteRepository;

    /// <summary>
    /// Конструктор обработчика запроса для получения заметок.
    /// </summary>
    /// <param name="noteRepository"> Репозиторий заметок, используемый для доступа к данным. </param>
    /// <exception cref="ArgumentNullException"> Выбрасывается, если <paramref name="noteRepository"/> равен null. </exception>
    public GetNotesQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    }

    /// <summary>
    /// Обрабатывает запрос на получение заметок.
    /// </summary>
    /// <param name="query"> Запрос на получение заметок, содержащий идентификатор пользователя, 
    /// поисковый запрос и параметры сортировки. </param>
    /// <param name="cancellationToken"> Токен отмены для прерывания выполнения задачи. </param>
    /// <returns> Асинхронная последовательность заметок, соответствующих запросу. </returns>
    public IAsyncEnumerable<Note> Handle(GetNotesQuery query, CancellationToken cancellationToken)
    {
        return _noteRepository.GetNotesAsync(query.UserId, query.SearchTerm, query.SortBy);
    }
}

using MediatR;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Queries.GetNotes;

public class GetNotesQueryHandler : IStreamRequestHandler<GetNotesQuery, Note>
{
    private readonly INoteRepository _taskRepository;

    public GetNotesQueryHandler(INoteRepository taskRepository)
    {
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    }

    public IAsyncEnumerable<Note> Handle(GetNotesQuery query, CancellationToken cancellationToken)
    {
        return _taskRepository.GetNotes(query.UserId);
    }
}

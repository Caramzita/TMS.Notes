using MediatR;
using TMS.Tasks.Core.Models;
using TMS.Tasks.UseCases.Abstractions;

namespace TMS.Tasks.UseCases.Queries.GetNotes;

public class GetNotesQueryHandler : IStreamRequestHandler<GetNotesQuery, Note>
{
    private readonly ITaskRepository _taskRepository;

    public GetNotesQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    }

    public IAsyncEnumerable<Note> Handle(GetNotesQuery query, CancellationToken cancellationToken)
    {
        return _taskRepository.GetNotes();
    }
}

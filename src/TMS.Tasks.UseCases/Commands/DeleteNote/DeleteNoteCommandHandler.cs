using AutoMapper;
using MediatR;
using TMS.Tasks.UseCases.Abstractions;

namespace TMS.Tasks.UseCases.Commands.DeleteNote;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly ITaskRepository _taskRepository;

    private readonly IMapper _mapper;

    public DeleteNoteCommandHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _taskRepository.GetNoteById(request.Id);

        if (note != null)
        {
            await _taskRepository.Delete(note).ConfigureAwait(false);
        }
    }
}

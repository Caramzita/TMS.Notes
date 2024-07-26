using AutoMapper;
using MediatR;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Commands.DeleteNote;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Unit>
{
    private readonly INoteRepository _taskRepository;

    private readonly IMapper _mapper;

    public DeleteNoteCommandHandler(INoteRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _taskRepository.GetNoteById(request.Id);

        if (entity != null && entity.UserId == request.UserId)
        {
            await _taskRepository.Delete(entity).ConfigureAwait(false);
        }

        return Unit.Value;
    }
}

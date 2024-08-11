using AutoMapper;
using MediatR;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;
using TMS.Notes.UseCases.Exceptions;

namespace TMS.Notes.UseCases.Notes.Commands.DeleteNote;

/// <summary>
/// Обработчик команды удаления заметки.
/// </summary>
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

        if (entity == null || entity.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Note), request.Id);
        }

        await _taskRepository.Delete(entity).ConfigureAwait(false);

        return Unit.Value;
    }
}

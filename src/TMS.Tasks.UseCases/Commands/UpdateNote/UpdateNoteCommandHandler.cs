using AutoMapper;
using MediatR;
using TMS.Tasks.Core.Models;
using TMS.Tasks.UseCases.Abstractions;

namespace TMS.Tasks.UseCases.Commands.UpdateNote;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Note>
{
    private readonly ITaskRepository _taskRepository;

    private readonly IMapper _mapper;

    public UpdateNoteCommandHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Note> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _taskRepository.GetNoteById(request.Id);

        note.UpdatedDate = DateTime.UtcNow;
        note.Title = request.Model.Title;
        note.Description = request.Model.Description;

        await _taskRepository.Update(note).ConfigureAwait(false);

        return note;
    }
}

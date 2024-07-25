using AutoMapper;
using MediatR;
using TMS.Tasks.Core.Models;
using TMS.Tasks.UseCases.Abstractions;

namespace TMS.Tasks.UseCases.Commands.AddNote;

public class AddNoteCommandHandler : IRequestHandler<AddNoteCommand, Note>
{
    private readonly ITaskRepository _taskRepository;

    private readonly IMapper _mapper;

    public AddNoteCommandHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository =  taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Note> Handle(AddNoteCommand request, CancellationToken cancellationToken)
    {
        var note = _mapper.Map<Note>(request.Model);
        note.Id = Guid.NewGuid();
        note.CreatedDate = DateTime.UtcNow;

        await _taskRepository.Add(note).ConfigureAwait(false);

        return note;
    }
}

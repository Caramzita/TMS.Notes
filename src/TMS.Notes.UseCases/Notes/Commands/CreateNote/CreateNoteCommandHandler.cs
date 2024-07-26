using AutoMapper;
using MediatR;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Commands.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Note>
{
    private readonly INoteRepository _taskRepository;

    private readonly IMapper _mapper;

    public CreateNoteCommandHandler(INoteRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Note> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note()
        {
            Id = Guid.NewGuid(),
            UserId = request.Model.UserId,
            Title = request.Model.Title,
            Description = request.Model.Description,
            CreationDate = DateTime.UtcNow,
            EditDate = DateTime.UtcNow,
        };

        await _taskRepository.Add(note).ConfigureAwait(false);

        return note;
    }
}

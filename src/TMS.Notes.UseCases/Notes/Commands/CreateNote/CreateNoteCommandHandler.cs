using AutoMapper;
using MediatR;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Commands.CreateNote;

/// <summary>
/// Обработчик добавления заметки.
/// </summary>
public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly INoteRepository _noteRepository;

    private readonly IMapper _mapper;

    public CreateNoteCommandHandler(INoteRepository noteRepository, IMapper mapper)
    {
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
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

        await _noteRepository.Add(note).ConfigureAwait(false);

        return note.Id;
    }
}

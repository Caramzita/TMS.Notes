using AutoMapper;
using MediatR;
using TMS.Application.UseCases;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Commands.CreateNote;

/// <summary>
/// Обработчик добавления заметки.
/// </summary>
public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Result<Guid>>
{
    private readonly INoteRepository _noteRepository;

    public CreateNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    }

    public async Task<Result<Guid>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note(request.Model.Title, request.Model.Description, request.Model.UserId);

        await _noteRepository.Add(note).ConfigureAwait(false);

        return Result<Guid>.SuccessfullyCreated(note.Id);
    }
}

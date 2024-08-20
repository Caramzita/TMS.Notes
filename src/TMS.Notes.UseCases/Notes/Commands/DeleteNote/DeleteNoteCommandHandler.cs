using AutoMapper;
using MediatR;
using TMS.Application.UseCases;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Commands.DeleteNote;

/// <summary>
/// Обработчик команды удаления заметки.
/// </summary>
public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Result<Unit>>
{
    private readonly INoteRepository _noteRepository;

    public DeleteNoteCommandHandler(INoteRepository noteRepository, IMapper mapper)
    {
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    }

    public async Task<Result<Unit>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _noteRepository.GetNoteById(request.Id);

        if (entity != null || entity!.UserId == request.UserId)
        {
            await _noteRepository.Delete(entity).ConfigureAwait(false);
        }

        return Result<Unit>.Empty();
    }
}

using AutoMapper;
using MediatR;
using TMS.Application.UseCases;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Commands.UpdateNote;

/// <summary>
/// Обработчик команды обновления заметки.
/// </summary>
public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Result<Note>>
{
    private readonly INoteRepository _noteRepository;

    private readonly IMapper _mapper;

    public UpdateNoteCommandHandler(INoteRepository noteRepository, IMapper mapper)
    {
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<Note>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _noteRepository.GetNoteById(request.Id);

        if (entity == null || entity.UserId != request.Model.UserId)
        {
            return Result<Note>.Invalid("note was't found");
        }

        entity.Update(request.Model.Title, request.Model.Description);

        await _noteRepository.Update(entity).ConfigureAwait(false);

        return Result<Note>.Success(entity);
    }
}

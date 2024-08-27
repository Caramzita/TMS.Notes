using AutoMapper;
using MediatR;
using TMS.Application.UseCases;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Commands.UpdateNote;

/// <summary>
/// Обработчик команды для обновления заметки.
/// </summary>
public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Result<Note>>
{
    /// <summary>
    /// Репозиторий заметок.
    /// </summary>
    private readonly INoteRepository _noteRepository;

    /// <summary>
    /// Маппер.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор обработчика команды для обновления заметки.
    /// </summary>
    /// <param name="noteRepository"> Репозиторий заметок, используемый для доступа к данным. </param>
    /// <param name="mapper"> Объект маппера для преобразования данных. </param>
    /// <exception cref="ArgumentNullException"> Выбрасывается, если <paramref name="noteRepository"/> 
    /// или <paramref name="mapper"/> равен null. </exception>
    public UpdateNoteCommandHandler(INoteRepository noteRepository, IMapper mapper)
    {
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Обрабатывает команду обновления заметки.
    /// </summary>
    /// <param name="request"> Команда для обновления заметки, содержащая идентификатор заметки и модель обновления. </param>
    /// <param name="cancellationToken"> Токен отмены для прерывания выполнения задачи. </param>
    /// <returns> Результат выполнения операции обновления заметки, включая обновленную заметку. </returns>
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

using AutoMapper;
using MediatR;
using TMS.Application.UseCases;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Commands.DeleteNote;

/// <summary>
/// Обработчик команды для удаления заметки.
/// </summary>
public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Result<Unit>>
{
    /// <summary>
    /// Репозиторий заметок.
    /// </summary>
    private readonly INoteRepository _noteRepository;

    /// <summary>
    /// Конструктор обработчика команды для удаления заметки.
    /// </summary>
    /// <param name="noteRepository"> Репозиторий заметок, используемый для доступа к данным. </param>
    /// <exception cref="ArgumentNullException"> Выбрасывается, если <paramref name="noteRepository"/> равен null. </exception>
    public DeleteNoteCommandHandler(INoteRepository noteRepository, IMapper mapper)
    {
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    }

    /// <summary>
    /// Обрабатывает команду удаления заметки.
    /// </summary>
    /// <param name="request"> Команда для удаления заметки, содержащая идентификатор заметки и идентификатор пользователя. </param>
    /// <param name="cancellationToken"> Токен отмены для прерывания выполнения задачи. </param>
    /// <returns> Результат выполнения операции удаления заметки. </returns>
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

using MediatR;
using TMS.Application.UseCases;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Notes.Commands.CreateNote;

/// <summary>
/// Обработчик команды для создания новой заметки.
/// </summary>
public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Result<Guid>>
{
    /// <summary>
    /// Репозиторий заметок.
    /// </summary>
    private readonly INoteRepository _noteRepository;

    /// <summary>
    /// Конструктор обработчика команды для создания заметки.
    /// </summary>
    /// <param name="noteRepository"> Репозиторий заметок, используемый для доступа к данным. </param>
    /// <exception cref="ArgumentNullException"> Выбрасывается, если <paramref name="noteRepository"/> равен null. </exception>
    public CreateNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    }

    /// <summary>
    /// Обрабатывает команду создания заметки.
    /// </summary>
    /// <param name="request"> Команда для создания новой заметки, содержащая данные заметки. </param>
    /// <param name="cancellationToken"> Токен отмены для прерывания выполнения задачи. </param>
    /// <returns> Результат выполнения операции, включающий идентификатор созданной заметки. </returns>
    public async Task<Result<Guid>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note(request.Model.Title, request.Model.Description, request.Model.UserId);

        await _noteRepository.Add(note).ConfigureAwait(false);

        return Result<Guid>.SuccessfullyCreated(note.Id);
    }
}

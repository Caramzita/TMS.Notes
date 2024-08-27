using FluentValidation;

namespace TMS.Notes.UseCases.Notes.Commands.UpdateNote;

/// <summary>
/// Валидатор команды обновления заметки.
/// </summary>
public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UpdateNoteCommandValidator"/>.
    /// </summary>
    public UpdateNoteCommandValidator()
    {
        RuleFor(command => command.Model)
            .NotNull()
            .WithMessage("Данные ввода не могут быть пустыми")
            .SetValidator(command => new NoteModelValidator());

        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Идентификатор заметки не должен быть пустым");
    }
}

using FluentValidation;

namespace TMS.Notes.UseCases.Notes.Commands.CreateNote;

/// <summary>
/// Валидатор команды добавления заметки.
/// </summary>
public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CreateNoteCommandValidator"/>.
    /// </summary>
    public CreateNoteCommandValidator()
    {
        RuleFor(command => command.Model)
            .NotNull()
            .WithMessage("Данные для ввода обязательны")
            .SetValidator(command => new NoteModelValidator());
    }
}

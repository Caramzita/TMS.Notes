using FluentValidation;

namespace TMS.Notes.UseCases.Notes.Commands.CreateNote;

/// <summary>
/// Валидатор команды добавления заметки.
/// </summary>
public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(command => command.Model)
            .NotNull()
            .WithMessage("Данные для ввода обязательны")
            .SetValidator(command => new NoteModelValidator());
    }
}

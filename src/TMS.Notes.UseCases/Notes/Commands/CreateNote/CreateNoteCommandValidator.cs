using FluentValidation;

namespace TMS.Notes.UseCases.Notes.Commands.CreateNote;

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

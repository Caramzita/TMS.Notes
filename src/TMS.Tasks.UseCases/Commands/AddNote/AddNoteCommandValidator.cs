using FluentValidation;

namespace TMS.Tasks.UseCases.Commands.AddNote;

public class AddNoteCommandValidator : AbstractValidator<AddNoteCommand>
{
    public AddNoteCommandValidator()
    {
        RuleFor(x => x.Model)
            .NotNull()
            .WithMessage("Данные для ввода обязательны")
            .SetValidator(x => new NoteModelValidator());
    }
}

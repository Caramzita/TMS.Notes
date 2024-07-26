using FluentValidation;

namespace TMS.Notes.UseCases.Notes.Commands.UpdateNote;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(command => command.Model)
            .NotNull()
            .WithMessage("Данные для ввода обязательны")
            .SetValidator(command => new NoteModelValidator());
    }
}

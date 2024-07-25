using FluentValidation;

namespace TMS.Tasks.UseCases.Commands.UpdateNote;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(x => x.Model)
            .NotNull()
            .WithMessage("Данные для ввода обязательны")
            .SetValidator(x => new NoteModelValidator());
    }
}

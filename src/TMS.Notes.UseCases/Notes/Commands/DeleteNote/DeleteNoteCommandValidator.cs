
using FluentValidation;

namespace TMS.Notes.UseCases.Notes.Commands.DeleteNote;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Идентификатор заметки не должен быть пустым");

        RuleFor(command => command.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("Идентификатор пользователя не должен быть пустым");
    }
}

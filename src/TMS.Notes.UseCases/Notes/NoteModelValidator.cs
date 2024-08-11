using FluentValidation;
using TMS.Notes.Core;

namespace TMS.Notes.UseCases.Notes;

/// <summary>
/// Валидатор модели заметки.
/// </summary>
public class NoteModelValidator : AbstractValidator<NoteInputModel>
{
    public NoteModelValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty()
            .MaximumLength(Note.MAX_LENGHT_TITLE)
            .WithMessage($"Заголовок не должен содержать больше {Note.MAX_LENGHT_TITLE} символов");

        RuleFor(model => model.Description)
            .MaximumLength(Note.MAX_LENGHT_DESCRIPTION)
            .WithMessage($"Содержание не должно содержать больше {Note.MAX_LENGHT_DESCRIPTION} символов");

        RuleFor(model => model.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("Идентификатор пользователя не должен быть пустым");
    }
}

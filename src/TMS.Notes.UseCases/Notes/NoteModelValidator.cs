using FluentValidation;

namespace TMS.Notes.UseCases.Notes;

public class NoteModelValidator : AbstractValidator<NoteInputModel>
{
    public NoteModelValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Заголовок не должен содержать больше 50 символов");

        RuleFor(model => model.Description)
            .MaximumLength(250)
            .WithMessage("Содержание не должно содержать больше 250 символов");

        RuleFor(model => model.UserId)
            .NotNull()
            .WithMessage("Идентификатор пользователя не должен быть пустым");
    }
}

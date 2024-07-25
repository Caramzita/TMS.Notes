using FluentValidation;

namespace TMS.Tasks.UseCases.Commands;

public class NoteModelValidator : AbstractValidator<NoteInputModel>
{
    public NoteModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Заголовок не должен содержать больше 50 символов");

        RuleFor(x => x.Description)
            .MaximumLength(250)
            .WithMessage("Содержание не должно содержать больше 250 символов");
    }
}

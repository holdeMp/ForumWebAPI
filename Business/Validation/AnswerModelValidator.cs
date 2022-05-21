
using Business.Models;
using FluentValidation;

namespace Business.Validation
{
    public class AnswerModelValidator : AbstractValidator<AnswerModel>
    {
        public AnswerModelValidator()
        {
            RuleFor(x=>x.ThemeId).NotNull();
            RuleFor(x=>x.AuthorId).NotNull();
            RuleFor(x=>x.Content).MinimumLength(3);
        }
    }
}

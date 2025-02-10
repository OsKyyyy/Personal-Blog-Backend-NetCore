using FluentValidation;
using Entities.Dtos.Project;

namespace Business.ValidationRules.FluentValidation.Project
{
    public class AddValidator : AbstractValidator<ProjectAddDto>
    {
        public AddValidator()
        {
            RuleFor(r => r.Title).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Content).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.CreateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}

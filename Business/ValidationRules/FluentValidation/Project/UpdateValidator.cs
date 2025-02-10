using FluentValidation;
using Entities.Dtos.Project;

namespace Business.ValidationRules.FluentValidation.Project
{
    public class UpdateValidator : AbstractValidator<ProjectUpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Title).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Content).NotEmpty().WithMessage("Bu alan boş olamaz");            
            RuleFor(r => r.UpdateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}

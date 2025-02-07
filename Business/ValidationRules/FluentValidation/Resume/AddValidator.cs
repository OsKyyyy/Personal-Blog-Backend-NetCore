using FluentValidation;
using Entities.Dtos.Resume;

namespace Business.ValidationRules.FluentValidation.Resume
{
    public class AddValidator : AbstractValidator<ResumeAddDto>
    {
        public AddValidator()
        {
            RuleFor(r => r.Description).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Title).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Organization).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.StartDate).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.CurrentPosition).NotEmpty().WithMessage("Bu alan boş olamaz");            
            RuleFor(r => r.CreateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}

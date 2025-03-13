using FluentValidation;
using Entities.Dtos.Resume;

namespace Business.ValidationRules.FluentValidation.Resume
{
    public class UpdateValidator : AbstractValidator<ResumeUpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Description).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Title).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Organization).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.StartDate).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.UpdateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}

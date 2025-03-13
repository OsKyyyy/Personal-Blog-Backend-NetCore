using FluentValidation;
using Entities.Dtos.Contact;

namespace Business.ValidationRules.FluentValidation.Contact
{
    public class UpdateValidator : AbstractValidator<ContactUpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Name).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Email).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Subject).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Message).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.UpdateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");            
        }
    }
}

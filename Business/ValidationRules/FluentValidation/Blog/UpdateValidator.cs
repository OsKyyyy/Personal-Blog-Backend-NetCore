using FluentValidation;
using Entities.Dtos.Blog;

namespace Business.ValidationRules.FluentValidation.Blog
{
    public class UpdateValidator : AbstractValidator<BlogUpdateDto>
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

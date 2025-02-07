using FluentValidation;
using Entities.Dtos.Blog;

namespace Business.ValidationRules.FluentValidation.Blog
{
    public class AddValidator : AbstractValidator<BlogAddDto>
    {
        public AddValidator()
        {
            RuleFor(r => r.Title).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Content).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.CreateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}

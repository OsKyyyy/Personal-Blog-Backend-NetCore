using FluentValidation;
using Entities.Dtos.Comment;

namespace Business.ValidationRules.FluentValidation.Comment
{
    public class UpdateValidator : AbstractValidator<CommentUpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.BlogId).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.CommentText).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Name).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Email).NotEmpty().WithMessage("Bu alan boş olamaz");                        
        }
    }
}

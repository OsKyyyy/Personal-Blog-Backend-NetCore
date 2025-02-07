using FluentValidation;
using Entities.Dtos.User;

namespace Business.ValidationRules.FluentValidation.User
{
    public class LoginValidator : AbstractValidator<UserLoginDto>
    {
        public LoginValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Bu alan boş olamaz").Must(r => r.Length > 7).WithMessage("Bu alan en az 8 karakter olmalıdır");
        }
    }
}

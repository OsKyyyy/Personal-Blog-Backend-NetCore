﻿using FluentValidation;
using Entities.Dtos.About;

namespace Business.ValidationRules.FluentValidation.About
{
    public class UpdateValidator : AbstractValidator<AboutUpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Title).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Content).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Name).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.DateOfBirth).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Address).NotEmpty().WithMessage("Bu alan boş olamaz");            
            RuleFor(r => r.Email).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Phone).Matches("^\\(?([1-9]{1})\\)?([0-9]{2})[. ]?([0-9]{3})[. ]?([0-9]{4})$").WithMessage("Bu alan ( 5XXXXXXXXX ) formatında olmalıdır. ");
            RuleFor(r => r.UpdateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}

﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos.About;

namespace Business.ValidationRules.FluentValidation.About
{
    public class AddValidator : AbstractValidator<AboutAddDto>
    {
        public AddValidator()
        {
            RuleFor(r => r.Title).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Content).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Name).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.DateOfBirth).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Address).NotEmpty().WithMessage("Bu alan boş olamaz");            
            RuleFor(r => r.Email).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Phone).Matches("^\\(?([1-9]{1})\\)?([0-9]{2})[. ]?([0-9]{3})[. ]?([0-9]{4})$").WithMessage("Bu alan ( 5XXXXXXXXX ) formatında olmalıdır. ");
            RuleFor(r => r.CreateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}

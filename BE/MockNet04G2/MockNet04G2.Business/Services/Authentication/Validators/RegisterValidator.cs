using FluentValidation;
using MockNet04G2.Business.DTOs.Authentication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Authentication.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Phone)
                .MaximumLength(15)
                .Matches(@"^\+?\d{10,15}$") 
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Dob)
                .LessThan(DateTime.Now)
                .WithMessage("Birthday must be in the past.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.ConfirmPassword)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}

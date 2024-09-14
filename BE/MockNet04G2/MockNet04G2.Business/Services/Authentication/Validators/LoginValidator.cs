using FluentValidation;
using MockNet04G2.Business.DTOs.Authentication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Authentication.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.EmailOrPhone).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

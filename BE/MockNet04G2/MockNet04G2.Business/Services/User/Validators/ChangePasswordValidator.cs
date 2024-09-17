using FluentValidation;
using MockNet04G2.Business.DTOs.Users.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.User.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().MinimumLength(6).MaximumLength(255);
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6).MaximumLength(255);
            RuleFor(x => x.ConfirmPassword).NotEmpty().MinimumLength(6).MaximumLength(255);
        }
    }
}

using FluentValidation;
using MockNet04G2.Business.DTOs.Users.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.User.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Name)
               .MaximumLength(100).WithMessage("Tên không thể dài quá 100 kí tự!")
               .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Phone)
                .Matches(@"^\d{10,15}$").WithMessage("Số điện thoại phải nằm trong khoảng 10 tới 15 kí tự!")
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));

            RuleFor(x => x.Dob)
                .LessThan(DateTime.Now).WithMessage("Ngày sinh phải ở quá khứ!")
                .When(x => x.Dob.HasValue);
        }
    }
}

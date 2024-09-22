using FluentValidation;
using MockNet04G2.Business.DTOs.Campaign.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin.Validators
{
    public class UpdateCampaignValidator : AbstractValidator<ExtendCampaignRequest>
    {
        public UpdateCampaignValidator()
        {
            RuleFor(x => x.EndDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Ngày kết thúc phải ở tương lai!");

            RuleFor(x => x.Limitation)
                .GreaterThan(0).WithMessage("Giới hạn quỹ phải là số dương!");
        }
    }
}

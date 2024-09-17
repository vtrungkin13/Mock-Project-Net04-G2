using FluentValidation;
using MockNet04G2.Business.DTOs.Campaign.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin.Validators
{
    public class CampaignDetailRequestValidator : AbstractValidator<CampaignDetailRequest>
    {
        public CampaignDetailRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot be longer than 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(255).WithMessage("Description cannot be longer than 255 characters.");

            RuleFor(x => x.Image)
                .MaximumLength(3000).WithMessage("Image URL cannot be longer than 3000 characters.")
                .Must(IsValidUrl).WithMessage("Image must be a valid URL.");

            RuleFor(x => x.Content)
                .MaximumLength(3000).WithMessage("Content cannot be longer than 3000 characters.");

            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Start date must be in the future.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after the start date.");

            RuleFor(x => x.Limitation)
                .GreaterThan(0).WithMessage("Limitation must be a positive value.");
        }

        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var _);
        }
    }
}

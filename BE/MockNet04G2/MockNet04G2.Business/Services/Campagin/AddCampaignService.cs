using AutoMapper;
using Azure.Core;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MockNet04G2.Business.DTOs.Campaign.Requests;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Business.MappingProfiles;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Models;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class AddCampaignService
    {
        private readonly IValidator<CampaignDetailRequest> _validator;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddCampaignService(IValidator<CampaignDetailRequest> validator, ICampaignRepository campaignRepository, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _campaignRepository = campaignRepository;
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CampaignMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ApiResponse<CampaignDetailReponse,string>> ExecuteAsync(CampaignDetailRequest request)
        {
            _validator.ValidateAndThrow(request);

            var response = new ApiResponse<CampaignDetailReponse, string>();

            //create a new campaign
            var newCampaign = new Campaign
            {
                Title = request.Title,
                Description = request.Description,
                Image = request.Image,
                Content = request.Content,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Limitation = request.Limitation,
                Status = StatusEnum.JustCreated,
                CreatedAt = DateTime.Now
            };

            _campaignRepository.Add(newCampaign);

            int saved = await _unitOfWork.SaveChangesAsync();

            if(saved <= 0)
            {
                response.Error = ErrorMessages.CannotAddCampaign;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }

            var campaignDetailResponse = _mapper.Map<CampaignDetailReponse>(newCampaign);

            response.Body = campaignDetailResponse;
            response.Status = StatusResponseEnum.Success;
            return response;

        }
    }
}

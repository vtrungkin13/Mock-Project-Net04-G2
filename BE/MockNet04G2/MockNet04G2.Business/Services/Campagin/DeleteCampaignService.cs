using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class DeleteCampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICooperateRepository _cooperateRepository;

        public DeleteCampaignService(IUnitOfWork unitOfWork, ICampaignRepository campaignRepository, ICooperateRepository cooperateRepository) 
        { 
            _unitOfWork = unitOfWork;
            _campaignRepository = campaignRepository;
            _cooperateRepository = cooperateRepository;
        }  

        public async Task<ApiResponse<string,string>> ExecuteAsync(int campaignId)
        {
            var response = new ApiResponse<string, string>();
            var campaign = await _campaignRepository.GetCampaignByIdAsync(campaignId);

            if (campaign == null)
            {
                response.Error = ErrorMessages.CampaginByIdNotFound;
                response.Status = Core.Common.Enums.StatusResponseEnum.InternalServerError;
                return response;
            }

            if (campaign.Status != StatusEnum.JustCreated)
            {
                response.Error = ErrorMessages.CannotDeleteCampaign;
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }

            var cooperations = await _cooperateRepository.FindByCampaignIdAsync(campaignId);
            if (cooperations.Any())
            {
                foreach (var cooperation in cooperations)
                {
                    _cooperateRepository.Delete(cooperation);
                }
            }

            // Delete the campaign
            _campaignRepository.Delete(campaign);

            int saved = await _unitOfWork.SaveChangesAsync();

            if (saved <= 0)
            {
                response.Error = ErrorMessages.CannotDeleteCampaign;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }

            response.Body = "Chiến dịch đã được xoá!";
            response.Status = StatusResponseEnum.Success;
            return response;
        }
    }
}

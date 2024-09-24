using AutoMapper;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Business.DTOs.Donations.Responses;
using MockNet04G2.Business.MappingProfiles;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Models;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Donate
{
    public class GetDonationHistoryService
    {

        private readonly IDonateRepository _donateRespository;
        private readonly IMapper _mapper;
        public GetDonationHistoryService(IDonateRepository donateRespository)
        {
            _donateRespository = donateRespository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DonateHistoryMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ApiResponse<List<Core.Models.Donate>, string>> ExecuteAsync(int uid)
        {
            var response = new ApiResponse<List<Core.Models.Donate>, string>();

            if (uid <= 0)
            {
                response.Error = "Please provide user id!";
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }

            List<Core.Models.Donate> donations = await _donateRespository.GetDonationHistory(uid);

            if(donations == null)
            {
                response.Error = "Can not get donate history!";
                response.Status = StatusResponseEnum.NotFound;
                return response;
            }

            //List<DonateHistoryResponseDto> donationsDto = _mapper.Map<List<DonateHistoryResponseDto>>(donations);

            response.Body = donations;
            response.Status = Core.Common.Enums.StatusResponseEnum.Success;

            return response;
        }
    }
}

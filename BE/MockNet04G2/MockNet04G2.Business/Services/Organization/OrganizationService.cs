using Azure;
using MockNet04G2.Business.DTOs.Organizations.Responses;
using MockNet04G2.Business.DTOs.Users.Responses;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Organization
{
    public class OrganizationService 
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<ApiResponse<List<OrganizationResponseDto>, string>> ExecuteAsync()
        {
            var response = new ApiResponse<List<OrganizationResponseDto>, string>();

            var organizations = await _organizationRepository.GetAllOrganizationAsync();

            var organizationResponseDtos = organizations.Select(org => new OrganizationResponseDto
            {
                Id = org.Id,
                Name = org.Name,
            }).ToList();

            response.Status = StatusResponseEnum.Success;
            response.Body = organizationResponseDtos;  
            return response;
        }
    }
}

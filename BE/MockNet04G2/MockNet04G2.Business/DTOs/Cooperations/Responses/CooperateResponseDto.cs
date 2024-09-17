using MockNet04G2.Business.DTOs.Organizations.Responses;
using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.DTOs.Cooperations.Responses
{
    public class CooperateResponseDto
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public virtual OrganizationResponseDto Organization { get; set; }
        public int CampaignId { get; set; }
        public virtual Core.Models.Campaign Campaign { get; set; }
    }
}

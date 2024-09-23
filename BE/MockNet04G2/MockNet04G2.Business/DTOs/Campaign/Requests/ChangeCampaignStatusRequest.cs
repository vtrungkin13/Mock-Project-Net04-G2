using MockNet04G2.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.DTOs.Campaign.Requests
{
    public class ChangeCampaignStatusRequest
    {
        public StatusEnum Status { get; set; }
    }
}


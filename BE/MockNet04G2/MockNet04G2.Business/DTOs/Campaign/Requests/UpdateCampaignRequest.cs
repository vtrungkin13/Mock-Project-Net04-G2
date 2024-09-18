using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.DTOs.Campaign.Requests
{
    public class UpdateCampaignRequest
    {
        public DateTime EndDate { get; set; }
        public decimal Limitation { get; set; }
    }
}

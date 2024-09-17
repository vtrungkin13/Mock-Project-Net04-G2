using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.DTOs.Campaign.Requests
{
    public class CampaignDetailRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Limitation { get; set; }

        public List<string> OrganizationNames { get; set; }
        public List<string> OrganizationPhones { get; set; }
    }
}

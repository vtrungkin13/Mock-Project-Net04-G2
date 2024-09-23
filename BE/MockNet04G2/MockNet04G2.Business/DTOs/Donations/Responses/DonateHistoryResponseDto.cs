using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Business.DTOs.Users.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.DTOs.Donations.Responses
{
    public class DonateHistoryResponseDto
    {
        public virtual CampaignResponseDto Campaign {  get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

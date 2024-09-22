using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.DTOs.Payments.Request
{
    public class PaymentRequest
    {
        public int UserId { get; set; }
        public int CampaignId { get; set; }
        public int Amount { get; set; }
    }
}

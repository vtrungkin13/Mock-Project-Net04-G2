using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockNet04G2.Business.DTOs.Donate.Responses;
using MockNet04G2.Business.DTOs.Cooperations.Responses;

namespace MockNet04G2.Business.DTOs.Campaign.Responses
{
    public class CampaignResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Limitation { get; set; }
        public StatusEnum Status { get; set; }
        public string Code
        {
            get
            {
                return CreatedAt.ToString() + Id.ToString();
            }
        }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<DonateResponseDto> Donations { get; set; }
        public virtual ICollection<CooperateResponseDto> Cooperations { get; set; }
    }
}

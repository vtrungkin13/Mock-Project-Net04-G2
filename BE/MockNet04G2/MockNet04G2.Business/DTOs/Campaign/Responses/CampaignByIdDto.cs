using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.DTOs.Campaign.Responses
{
    public class CampaignByIdDto
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

        public virtual ICollection<CampaignByIdDonationsDto> Donations { get; set; }
        public virtual ICollection<CampaignByIdCooperationsDto> Cooperations { get; set; }
    }

    public class CampaignByIdDonationsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual CampaignByIdUserDto User { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class CampaignByIdUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Dob { get; set; }
        public RoleEnum Role { get; set; }
    }

    public class CampaignByIdCooperationsDto
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public virtual CampaignByIdOrganizationDto Organization { get; set; }
    }

    public class CampaignByIdOrganizationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
    }
}

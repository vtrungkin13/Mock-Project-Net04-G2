using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockNet04G2.Business.DTOs.Users.Responses;

namespace MockNet04G2.Business.DTOs.Donate.Responses
{
    public class DonateResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual UserDetailDto User { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

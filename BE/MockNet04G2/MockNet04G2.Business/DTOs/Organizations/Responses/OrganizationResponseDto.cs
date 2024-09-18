using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.DTOs.Organizations.Responses
{
    public class OrganizationResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public virtual ICollection<Cooperate> Cooperations { get; set; }
    }
}

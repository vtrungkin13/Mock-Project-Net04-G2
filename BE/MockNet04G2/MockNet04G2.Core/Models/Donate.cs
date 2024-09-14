using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Models
{
    public class Donate
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Campaign")]
        public int CampaignId { get; set; }

        public virtual Campaign Campaign { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(5000, double.MaxValue, ErrorMessage = "Số tiền quyên góp ít nhất là 5000đ")]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}

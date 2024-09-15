using MockNet04G2.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Models
{
    public class Campaign
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(3000)]
        public string Image { get; set; }

        [StringLength(3000)]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Limitation { get; set; }

        public StatusEnum Status { get; set; }

        [StringLength(255)]
        public string OrganizationName { get; set; }

        [StringLength(15)]
        public string OrganizationPhone { get; set; }

        public string Code
        {
            get
            {
                return CreatedAt.ToString() + Id.ToString();
            }
        }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Donate> Donations { get; set; }
    }
}

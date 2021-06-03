using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class Equipment : ModelBase
    {
        public Equipment()
        {
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Type { get; set; }
        [StringLength(255)]
        public string Make { get; set; }
        [StringLength(255)]
        public string Model { get; set; }
        [Required]
        [StringLength(100)]
        public string Serial { get; set; }
        [StringLength(500)]
        public string features { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        public List<TicketsEquipments> Tickets { get; set; }

    }
}

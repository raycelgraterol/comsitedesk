using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class TicketCategories : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}

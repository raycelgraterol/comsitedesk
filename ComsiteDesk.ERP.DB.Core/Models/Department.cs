using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class Department : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [Required]
        public int HeadquarterId { get; set; }
        public Headquarter Headquarter { get; set; }
    }
}

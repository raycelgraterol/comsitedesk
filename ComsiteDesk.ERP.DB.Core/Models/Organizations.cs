using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class Organizations
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BusinessName { get; set; }        
        public string RIF { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

    }
}

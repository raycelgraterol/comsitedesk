using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class TaskStatus : ModelBase 
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
    }
}

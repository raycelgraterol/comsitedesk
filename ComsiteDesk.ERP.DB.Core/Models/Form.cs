using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class Form : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(255)]
        public string URI { get; set; }

        [Required]
        public int ModuleId { get; set; }
        public Module Module { get; set; }

        [NotMapped]
        public string ModuleName { get => this.Module == null ? "" : this.Module.Name; }

        public List<FormAction> Actions { get; set; }
    }
}

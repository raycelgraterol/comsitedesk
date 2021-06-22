using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class Projects : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        [Required]
        public int ProjectStatusId { get; set; }
        [NotMapped]
        public string ProjectStatusName
        {
            get
            {
                return ProjectStatus != null ? ProjectStatus.Name : "";
            }
        }
        public ProjectStatus ProjectStatus { get; set; }
    }
}

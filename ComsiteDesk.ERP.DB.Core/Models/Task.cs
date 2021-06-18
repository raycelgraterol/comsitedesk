using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class Task : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int ProjectsId { get; set; }
        public Projects Projects { get; set; }

        [Required]
        public int TaskStatusId { get; set; }
        public TaskStatus TaskStatus { get; set; }
    }
}

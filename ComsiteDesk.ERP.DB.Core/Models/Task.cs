using ComsiteDesk.ERP.DB.Core.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public string TaskStatusName
        {
            get
            {
                return TaskStatus != null ? TaskStatus.Name : "";
            }
        }

        [Required]
        public long UserId { get; set; }
        public User User { get; set; }

        [NotMapped]
        public string UserName
        {
            get
            {
                return User != null ? User.FullName : "";
            }
        }

        [NotMapped]
        public string UserImageUrl
        {
            get
            {
                return User != null ? User.ImageUrl : "";
            }
        }
    }
}

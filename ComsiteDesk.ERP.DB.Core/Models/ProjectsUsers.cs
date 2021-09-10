using ComsiteDesk.ERP.DB.Core.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class ProjectsUsers : ModelBase
    {
        public int ProjectsId { get; set; }
        public Projects Projects { get; set; }

        [NotMapped]
        public string ProjectsName
        {
            get
            {
                return Projects != null ? Projects.Title : "";
            }
        }

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

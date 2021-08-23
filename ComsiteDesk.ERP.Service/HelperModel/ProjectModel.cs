using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class ProjectModel : ModelBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        public string OrganizationName { get; set; }

        public int ProjectStatusId { get; set; }
        public string ProjectStatusName { get; set; }

        public ProjectStatus ProjectStatus { get; set; }
        public int TotalTasks { get; set; }
    }
}

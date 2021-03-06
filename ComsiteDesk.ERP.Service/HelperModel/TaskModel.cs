using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class TaskModel : ModelBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public int ProjectsId { get; set; }
        //public Projects Projects { get; set; }
        public int TaskStatusId { get; set; }
        //public TaskStatus TaskStatus { get; set; }
    }
}

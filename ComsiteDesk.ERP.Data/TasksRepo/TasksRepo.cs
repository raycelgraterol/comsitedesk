using ComsiteDesk.ERP.DB.Core;
using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Data
{
    public class TasksRepo : Repository<Task>, ITasksRepo
    {
        public TasksRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}

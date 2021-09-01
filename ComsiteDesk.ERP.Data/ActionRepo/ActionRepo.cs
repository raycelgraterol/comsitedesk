using ComsiteDesk.ERP.DB.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Action = ComsiteDesk.ERP.DB.Core.Models.Action;

namespace ComsiteDesk.ERP.Data
{
    public class ActionRepo : Repository<Action>, IActionRepo
    {
        public ActionRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Action = ComsiteDesk.ERP.DB.Core.Models.Action;

namespace ComsiteDesk.ERP.Data
{
    public interface IActionRepo : IRepository<Action>
    {
    }
}

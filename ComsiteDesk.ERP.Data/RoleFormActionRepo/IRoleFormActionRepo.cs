using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComsiteDesk.ERP.Data
{
    public interface IRoleFormActionRepo : IRepository<RoleFormAction>
    {
        IQueryable<RoleFormAction> GetAllRaw();
    }
}

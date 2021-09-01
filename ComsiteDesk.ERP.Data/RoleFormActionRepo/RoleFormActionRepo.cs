using ComsiteDesk.ERP.DB.Core;
using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComsiteDesk.ERP.Data
{
    public class RoleFormActionRepo : Repository<RoleFormAction>, IRoleFormActionRepo
    {
        public RoleFormActionRepo(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<RoleFormAction> GetAllRaw()
        {
            return _applicationDbContext.RoleFormAction;
        }
    }
}

using ComsiteDesk.ERP.DB.Core;
using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Data
{
    public class TicketTypesRepo : Repository<TicketTypes>, ITicketTypesRepo
    {
        public TicketTypesRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}

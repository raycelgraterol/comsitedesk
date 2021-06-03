using ComsiteDesk.ERP.DB.Core;
using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Data
{
    public class TicketCategoriesRepo : Repository<TicketCategories>, ITicketCategoriesRepo
    {
        public TicketCategoriesRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}

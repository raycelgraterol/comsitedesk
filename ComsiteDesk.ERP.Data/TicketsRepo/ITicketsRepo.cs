using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Data
{
    public interface ITicketsRepo : IRepository<Tickets>
    {
        IQueryable<Tickets> GetAllTicketsUsers();
    }
}

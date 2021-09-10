using ComsiteDesk.ERP.DB.Core;
using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Data
{
    public class TicketsEquipmentsRepo : Repository<TicketsEquipments>, ITicketsEquipmentsRepo
    {
        public TicketsEquipmentsRepo(ApplicationDbContext context) : base(context)
        {

        }
    }
}

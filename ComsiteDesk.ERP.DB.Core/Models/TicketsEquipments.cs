using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class TicketsEquipments
    {
        public int TicketsId { get; set; }
        public Tickets Tickets { get; set; }

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
    }
}

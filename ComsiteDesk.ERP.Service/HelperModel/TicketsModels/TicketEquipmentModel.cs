using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class TicketEquipmentModel : ModelBase
    {
        public int TicketsId { get; set; }
        public string TicketsName { get; set; }

        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
    }
}

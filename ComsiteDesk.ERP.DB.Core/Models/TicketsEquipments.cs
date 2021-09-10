using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class TicketsEquipments: ModelBase
    {
        public int TicketsId { get; set; }
        public Tickets Tickets { get; set; }

        [NotMapped]
        public string TicketsName
        {
            get
            {
                return Tickets != null ? Tickets.Title : "";
            }
        }

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        [NotMapped]
        public string EquipmentName
        {
            get
            {
                return Equipment != null ? Equipment.Name : "";
            }
        }
    }
}

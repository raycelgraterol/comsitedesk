using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class EquipmentModel
    {        
        public int Id { get; set; }        
        public string Name { get; set; }        
        public string Type { get; set; }        
        public string Make { get; set; }        
        public string Model { get; set; }        
        public string Serial { get; set; }        
        public string features { get; set; }        
        public string Notes { get; set; }
        
        public int OrganizationId { get; set; }
        public OrganizationModel Organization { get; set; }
        //public List<TicketsEquipments> Tickets { get; set; }
    }
}

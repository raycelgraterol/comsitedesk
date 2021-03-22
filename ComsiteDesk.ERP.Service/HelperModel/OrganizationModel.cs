using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class OrganizationModel
    {        
        public int Id { get; set; }        
        public string BusinessName { get; set; }
        public string RIF { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}

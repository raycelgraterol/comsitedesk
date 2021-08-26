using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class HeadquarterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public int OrganizationsId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int HeadquarterId { get; set; }
    }
}

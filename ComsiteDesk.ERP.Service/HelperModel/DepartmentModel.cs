using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class DepartmentModel: ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int HeadquarterId { get; set; }
        public string HeadquarterName { get; set; }
    }
}

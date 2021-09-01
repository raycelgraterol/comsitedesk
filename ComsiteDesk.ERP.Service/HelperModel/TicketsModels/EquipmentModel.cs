using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class EquipmentModel : ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string Features { get; set; }
        public string Notes { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int EquipmentUserId { get; set; }
        public string EquipmentUserName { get; set; }

    }
}

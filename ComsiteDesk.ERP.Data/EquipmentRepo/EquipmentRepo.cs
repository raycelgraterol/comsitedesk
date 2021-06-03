using ComsiteDesk.ERP.DB.Core;
using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Data
{
    public class EquipmentRepo : Repository<Equipment>, IEquipmentRepo
    {
        public EquipmentRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}

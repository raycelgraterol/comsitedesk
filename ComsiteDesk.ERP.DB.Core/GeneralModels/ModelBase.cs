using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class ModelBase : IModelBase
    {
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}

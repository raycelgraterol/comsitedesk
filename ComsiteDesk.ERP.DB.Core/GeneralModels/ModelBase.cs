using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class ModelBase : IModelBase
    {
        public ModelBase()
        {
            IsActive = true;
        }
        public DateTime DateCreated { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public long? ModifiedBy { get; set; }       
        public bool IsActive { get; set; }
    }
}

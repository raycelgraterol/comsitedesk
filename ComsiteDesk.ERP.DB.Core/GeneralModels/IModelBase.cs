using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public interface IModelBase
    {
        public bool IsActive { get; set; }
    }
}

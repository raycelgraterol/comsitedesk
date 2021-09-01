using ComsiteDesk.ERP.DB.Core.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class RoleFormAction : ModelBase
    {
        public int FormActionId { get; set; }
        
        public FormAction FormAction { get; set; }

        public long RoleId { get; set; }
        public Role Role { get; set; }
        
        [NotMapped]
        public string FormName
        { 
            get 
            {
                return this.FormAction.Form == null ? "" : this.FormAction.Form.Name; 
            }
        }

        [NotMapped]
        public string ActionName
        {
            get
            {
                return this.FormAction.Action == null ? "" : this.FormAction.Action.Name;
            }
        }
    }
}

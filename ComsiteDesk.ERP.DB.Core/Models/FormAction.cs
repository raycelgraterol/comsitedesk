using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class FormAction : ModelBase
    {
        [Key]
        public int Id { get; set; }
        public int FormId { get; set; }
        [NotMapped]
        public string FormName {
            get
            {
                if (Form != null)
                {
                    return Form.Name;
                }
                return "";
            }
        }
        public Form Form { get; set; }

        public int ActionId { get; set; }
        [NotMapped]
        public string ActionName { 
            get 
            {
                if (Action != null)
                {
                    return Action.Name;
                }
                return "";
            } 
        }
        public Action Action { get; set; }
        public List<RoleFormAction> Roles { get; set; }
    }
}

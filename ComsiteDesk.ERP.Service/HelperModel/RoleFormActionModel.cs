using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class RoleFormActionModel
    {
        /// <summary>
        /// Form Action Id Table
        /// </summary>
        public int FormActionId { get; set; }
        /// <summary>
        /// Role Id Table
        /// </summary>
        public long RoleId { get; set; }

        public bool IsActive { get; set; }

        public string RoleName { get; set; }

        public int ModuleId { get; set; }
        public string ModuleName { get; set; }

        public int FormId { get; set; }
        public string FormName { get; set; }

        /// <summary>
        /// URI Module just used to check 
        /// </summary>
        public string URIModule { get; set; }
        /// <summary>
        /// URI Form just used to check
        /// </summary>
        public string URIForm { get; set; }
        /// <summary>
        /// Action Name just for check
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Just for save array actions
        /// </summary>
        public List<string> ActionsName { get; set; }

    }

    public class RoleFormArrayAction
    {
        public long RoleId { get; set; }
        public int FormId { get; set; }
        public int[] formActionIds { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class FormActionModel
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string FormName { get; set; }

        public int ActionId { get; set; }
        public string ActionName { get; set; }

        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}

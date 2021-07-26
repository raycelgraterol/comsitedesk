using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class ChangeLog : ModelBase
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string EventType { get; set; }
        [StringLength(255)]
        public string EventLocation { get; set; }
        public string ExceptionMessage { get; set; }
        public DateTime EventDate { get; set; }
        [StringLength(255)]
        public string LoginName { get; set; }
        [StringLength(500)]
        public string GroupGUID { get; set; }
    }
}

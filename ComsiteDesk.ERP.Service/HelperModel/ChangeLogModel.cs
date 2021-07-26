using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class ChangeLogModel
    {
        public long Id { get; set; }
        public string EventType { get; set; }
        public string EventLocation { get; set; }
        public string ExceptionMessage { get; set; }
        public DateTime EventDate { get; set; }
        public string LoginName { get; set; }
        public string GroupGUID { get; set; }
    }
}

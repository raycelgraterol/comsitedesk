using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class TicketsSearchModel : SearchParameters
    {
        public DateTime StartDate { get; set; }
        public long AssignedTo { get; set; }
        public int TicketStatusId { get; set; }
    }
}

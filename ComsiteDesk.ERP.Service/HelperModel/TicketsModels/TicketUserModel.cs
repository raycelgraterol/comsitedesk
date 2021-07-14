using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class TicketUserModel : ModelBase
    {
        public int TicketsId { get; set; }
        public string TicketsName { get; set; }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserImageUrl { get; set; }
    }
}

using ComsiteDesk.ERP.DB.Core.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class TicketsUsers
    {
        public int TicketsId { get; set; }
        public Tickets Tickets { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}

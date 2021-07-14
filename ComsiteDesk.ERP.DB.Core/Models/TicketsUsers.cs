using ComsiteDesk.ERP.DB.Core.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class TicketsUsers : ModelBase
    {
        public int TicketsId { get; set; }
        public Tickets Tickets { get; set; }

        [NotMapped]
        public string TicketsName
        {
            get
            {
                return Tickets != null ? Tickets.Title : "";
            }
        }

        public long UserId { get; set; }
        public User User { get; set; }

        [NotMapped]
        public string UserName
        {
            get
            {
                return User != null ? User.FullName : "";
            }
        }

        [NotMapped]
        public string UserImageUrl
        {
            get
            {
                return User != null ? User.ImageUrl : "";
            }
        }

    }
}

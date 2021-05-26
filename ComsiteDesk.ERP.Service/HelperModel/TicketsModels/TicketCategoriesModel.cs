using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class TicketCategoriesModel : ModelBase
    {        
        public int Id { get; set; }        
        public string Name { get; set; }
    }
}

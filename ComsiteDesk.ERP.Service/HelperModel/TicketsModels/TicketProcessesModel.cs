﻿using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class TicketProcessesModel : ModelBase
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public int Step { get; set; }
    }
}

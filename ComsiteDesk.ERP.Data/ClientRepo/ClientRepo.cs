using ComsiteDesk.ERP.DB.Core;
using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Data
{
    public class ClientRepo : Repository<Client>, IClientRepo
    {
        public ClientRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}

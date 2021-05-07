using ComsiteDesk.ERP.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service
{
    public interface IUnitOfWork
    {
        void Commit();
        IOrganizationsRepo OrganizationsRepo { get; }
    }
}

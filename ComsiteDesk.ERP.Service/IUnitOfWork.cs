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
        ITicketTypesRepo TicketTypesRepo { get; }
        ITicketStatusRepo TicketStatusRepo { get; }
        ITicketsRepo TicketsRepo { get; }
        ITicketProcessesRepo TicketProcessesRepo { get; }
        ITicketCategoriesRepo TicketCategoriesRepo { get; }
        IEquipmentRepo EquipmentRepo { get; }
    }
}

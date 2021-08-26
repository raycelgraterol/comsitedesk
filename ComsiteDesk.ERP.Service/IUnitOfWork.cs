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
        ITicketsUsersRepo TicketsUsersRepo { get; }
        ITicketProcessesRepo TicketProcessesRepo { get; }
        ITicketCategoriesRepo TicketCategoriesRepo { get; }
        IEquipmentRepo EquipmentRepo { get; }
        IProjectsRepo ProjectsRepo { get; }
        IProjectStatusRepo ProjectStatusRepo { get; }
        ITasksRepo TasksRepo { get; }
        ITaskStatusRepo TaskStatusRepo { get; }
        IChangeLogRepo ChangeLogRepo { get; }
        IHeadquarterRepo HeadquarterRepo { get; }
        IDepartmentRepo DepartmentRepo { get; }
        IEquipmentUserRepo EquipmentUserRepo  { get; }
    }
}

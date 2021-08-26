using ComsiteDesk.ERP.Data;
using ComsiteDesk.ERP.DB.Core;
using System;

namespace ComsiteDesk.ERP.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private IOrganizationsRepo _organizationsRepo { get; set; }
        private ITicketTypesRepo _ticketTypesRepo { get; set; }
        private ITicketStatusRepo _ticketStatusRepo { get; set; }
        private ITicketsRepo _ticketsRepo { get; set; }
        private ITicketsUsersRepo _ticketsUsersRepo { get; set; }
        private ITicketProcessesRepo _ticketProcessesRepo { get; set; }
        private ITicketCategoriesRepo _ticketCategoriesRepo { get; set; }
        private IEquipmentRepo _equipmentRepo { get; set; }
        private IProjectsRepo _projectsRepo { get; set; }
        private IProjectStatusRepo _projectStatusRepo { get; set; }
        private ITasksRepo _tasksRepo { get; set; }
        private ITaskStatusRepo _taskStatusRepo { get; set; }
        private IChangeLogRepo _changeLogRepo { get; }
        private IDepartmentRepo _departmentRepo { get; }
        private IHeadquarterRepo _headquarterRepo { get; }
        private IEquipmentUserRepo _equipmentUserRepo { get; }

        public UnitOfWork(ApplicationDbContext db,
            IOrganizationsRepo organizationsRepo,
            ITicketTypesRepo ticketTypesRepo,
            ITicketStatusRepo ticketStatusRepo,
            ITicketsRepo ticketsRepo,
            ITicketsUsersRepo ticketsUsersRepo,
            ITicketProcessesRepo ticketProcessesRepo,
            ITicketCategoriesRepo ticketCategoriesRepo,
            IEquipmentRepo equipmentRepo,
            IProjectsRepo projectsRepo,
            IProjectStatusRepo projectStatusRepo,
            ITasksRepo tasksRepo,
            ITaskStatusRepo taskStatusRepo,
            IChangeLogRepo changeLogRepo,
            IDepartmentRepo departmentRepo,
            IHeadquarterRepo headquarterRepo,
            IEquipmentUserRepo equipmentUserRepo)
        {
            _db = db;
            _organizationsRepo = organizationsRepo;
            _ticketTypesRepo = ticketTypesRepo;
            _ticketStatusRepo = ticketStatusRepo;
            _ticketsRepo = ticketsRepo;
            _ticketsUsersRepo = ticketsUsersRepo;
            _ticketProcessesRepo = ticketProcessesRepo;
            _ticketCategoriesRepo = ticketCategoriesRepo;
            _ticketCategoriesRepo = ticketCategoriesRepo;
            _equipmentRepo = equipmentRepo;
            _projectsRepo = projectsRepo;
            _projectStatusRepo = projectStatusRepo;
            _tasksRepo = tasksRepo;
            _taskStatusRepo = taskStatusRepo;
            _changeLogRepo = changeLogRepo;
            _departmentRepo = departmentRepo;
            _headquarterRepo = headquarterRepo;
            _equipmentUserRepo = equipmentUserRepo;
        }

        private ApplicationDbContext _db { get; set; }
        public IOrganizationsRepo OrganizationsRepo => _organizationsRepo;
        public ITicketTypesRepo TicketTypesRepo => _ticketTypesRepo;
        public ITicketStatusRepo TicketStatusRepo => _ticketStatusRepo;
        public ITicketsRepo TicketsRepo => _ticketsRepo;
        public ITicketsUsersRepo TicketsUsersRepo => _ticketsUsersRepo;
        public ITicketProcessesRepo TicketProcessesRepo => _ticketProcessesRepo;
        public ITicketCategoriesRepo TicketCategoriesRepo => _ticketCategoriesRepo;
        public IEquipmentRepo EquipmentRepo => _equipmentRepo;
        public IProjectsRepo ProjectsRepo => _projectsRepo;
        public IProjectStatusRepo ProjectStatusRepo => _projectStatusRepo;
        public ITasksRepo TasksRepo => _tasksRepo;
        public ITaskStatusRepo TaskStatusRepo => _taskStatusRepo;
        public IChangeLogRepo ChangeLogRepo => _changeLogRepo;
        public IDepartmentRepo DepartmentRepo => _departmentRepo;
        public IHeadquarterRepo HeadquarterRepo => _headquarterRepo;
        public IEquipmentUserRepo EquipmentUserRepo => _equipmentUserRepo;

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}

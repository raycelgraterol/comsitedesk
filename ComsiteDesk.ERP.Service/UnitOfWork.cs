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
        private ITicketProcessesRepo _ticketProcessesRepo { get; set; }
        private ITicketCategoriesRepo _ticketCategoriesRepo { get; set; }
        private IEquipmentRepo _equipmentRepo { get; set; }


        public UnitOfWork(ApplicationDbContext db,
            IOrganizationsRepo organizationsRepo,
            ITicketTypesRepo ticketTypesRepo,
            ITicketStatusRepo ticketStatusRepo,
            ITicketsRepo ticketsRepo,
            ITicketProcessesRepo ticketProcessesRepo,
            ITicketCategoriesRepo ticketCategoriesRepo,
            IEquipmentRepo equipmentRepo)
        {
            _db = db;
            _organizationsRepo = organizationsRepo;
            _ticketTypesRepo = ticketTypesRepo;
            _ticketStatusRepo = ticketStatusRepo;
            _ticketsRepo = ticketsRepo;
            _ticketProcessesRepo = ticketProcessesRepo;
            _ticketCategoriesRepo = ticketCategoriesRepo;
            _ticketCategoriesRepo = ticketCategoriesRepo;
            _equipmentRepo = equipmentRepo;
        }

        private ApplicationDbContext _db { get; set; }
        public IOrganizationsRepo OrganizationsRepo => _organizationsRepo;
        public ITicketTypesRepo TicketTypesRepo => _ticketTypesRepo;
        public ITicketStatusRepo TicketStatusRepo => _ticketStatusRepo;
        public ITicketsRepo TicketsRepo => _ticketsRepo;
        public ITicketProcessesRepo TicketProcessesRepo => _ticketProcessesRepo;
        public ITicketCategoriesRepo TicketCategoriesRepo => _ticketCategoriesRepo;
        public IEquipmentRepo EquipmentRepo => _equipmentRepo;


        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}

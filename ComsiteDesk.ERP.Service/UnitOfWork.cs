using ComsiteDesk.ERP.Data;
using ComsiteDesk.ERP.DB.Core;
using System;

namespace ComsiteDesk.ERP.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private IClientsRepo _clientsRepo { get; set; }
        private IOrganizationsRepo _organizationsRepo { get; set; }
        public UnitOfWork(ApplicationDbContext db,
            IClientsRepo clientsRepo)
        {
            _db = db;
            _clientsRepo = clientsRepo;

        }

        private ApplicationDbContext _db { get; set; }
        public IClientsRepo ClientRepo => _clientsRepo;
        public IOrganizationsRepo OrganizationsRepo => _organizationsRepo;


        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}

using ComsiteDesk.ERP.Data;
using ComsiteDesk.ERP.DB.Core;
using System;

namespace ComsiteDesk.ERP.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private IOrganizationsRepo _organizationsRepo { get; set; }
        public UnitOfWork(ApplicationDbContext db,
            IOrganizationsRepo organizationsRepo)
        {
            _db = db;
            _organizationsRepo = organizationsRepo;

        }

        private ApplicationDbContext _db { get; set; }
        public IOrganizationsRepo OrganizationsRepo => _organizationsRepo;


        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}

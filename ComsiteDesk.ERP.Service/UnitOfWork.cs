using ComsiteDesk.ERP.DB.Core;
using System;

namespace ComsiteDesk.ERP.Service
{
    public class UnitOfWork : IUnitOfWork
    {        
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            
        }

        private ApplicationDbContext _db { get; set; }
        

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}

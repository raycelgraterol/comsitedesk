using ComsiteDesk.ERP.DB.Core;
using ComsiteDesk.ERP.DB.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IModelBase, new()
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return _applicationDbContext.Set<TEntity>().Where(x => x.IsActive);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Insert)} entity must not be null");
            }

            try
            {
                await _applicationDbContext.AddAsync(entity);

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public TEntity Edit(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Insert)} entity must not be null");
            }

            try
            {
                foreach (var entityIn in _applicationDbContext.ChangeTracker.Entries())
                {
                    entityIn.State = EntityState.Detached;
                }

                _applicationDbContext.Entry(entity).State = EntityState.Modified;

                _applicationDbContext.Update(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        public async Task<TEntity> GetById(int id)
        {
            try
            {
                return await _applicationDbContext.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"{id} could not be get: {ex.Message}");
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Insert)} entity must not be null");
            }

            try
            {
                _applicationDbContext.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
    }
}

using ComsiteDesk.ERP.Service.HelperModel;
using ComsiteDesk.ERP.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using dbModels = ComsiteDesk.ERP.DB.Core.Models;

namespace ComsiteDesk.ERP.Service
{
    public class TaskService : ITaskService
    {
        public IUnitOfWork _uow;

        public TaskService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(TaskModel itemModel)
        {
            dbModels.Task _item = CoreMapper.MapObject<TaskModel, dbModels.Task>(itemModel);
            _item.IsActive = true;
            await _uow.TasksRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<TaskModel>> GetAllAsync()
        {
            List<TaskModel> items =
                CoreMapper.MapList<dbModels.Task, TaskModel>(await _uow.TasksRepo.GetAll().ToListAsync());

            return items;
        }

        public List<TaskModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.TasksRepo.GetAll()
                                .Include(x => x.TaskStatus)
                                .Include(x => x.User)
                                .AsQueryable();

                //count all items
                searchParameters.totalCount = result.Count();

                searchParameters.searchTerm =
                    searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                result = result.Where(s =>
                            (searchParameters.searchTerm == "") ||
                            s.Title.ToLower().Contains(searchParameters.searchTerm.ToLower()) ||
                            s.Id.ToString().Contains(searchParameters.searchTerm.ToLower()));

                if (searchParameters.sortColumn != null)
                {
                    //Sorting
                    result = result.AsQueryable()
                            .OrderBy(searchParameters.sortColumn + " " + searchParameters.sortDirection);
                }
                else
                {
                    result = result.OrderBy(x => x.Id);
                }

                result = result
                            .Skip(searchParameters.startIndex)
                            .Take(searchParameters.PageSize);

                List<TaskModel> itemsModels =
                    CoreMapper.MapList<dbModels.Task, TaskModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TaskModel> GetById(int itemId)
        {
            dbModels.Task result = await _uow.TasksRepo.GetAll().Include(t => t.TaskStatus)
                                                .Include(t => t.User)
                                                .FirstOrDefaultAsync(x => x.Id == itemId);

            TaskModel itemModel =
                CoreMapper.MapObject<dbModels.Task, TaskModel>(result);

            return itemModel;
        }

        public int Remove(TaskModel itemModel)
        {
            dbModels.Task item = CoreMapper.MapObject<TaskModel, dbModels.Task>(itemModel);
            item.IsActive = false;
            _uow.TasksRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(TaskModel itemModel)
        {
            dbModels.Task item = CoreMapper.MapObject<TaskModel, dbModels.Task>(itemModel);
            item.IsActive = true;
            _uow.TasksRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

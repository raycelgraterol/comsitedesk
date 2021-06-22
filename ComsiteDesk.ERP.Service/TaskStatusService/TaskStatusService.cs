using ComsiteDesk.ERP.DB.Core.Models;
using ComsiteDesk.ERP.Service.HelperModel;
using ComsiteDesk.ERP.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public class TaskStatusService : ITaskStatusService
    {
        public IUnitOfWork _uow;

        public TaskStatusService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(TaskStatusModel itemModel)
        {
            DB.Core.Models.TaskStatus _item = CoreMapper.MapObject<TaskStatusModel, DB.Core.Models.TaskStatus>(itemModel);
            _item.IsActive = true;
            await _uow.TaskStatusRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<TaskStatusModel>> GetAllAsync()
        {
            List<TaskStatusModel> items =
                CoreMapper.MapList<DB.Core.Models.TaskStatus, TaskStatusModel>(await _uow.TaskStatusRepo.GetAll().ToListAsync());

            return items;
        }

        public List<TaskStatusModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.TaskStatusRepo.GetAll();

                //count all items
                searchParameters.totalCount = result.Count();

                searchParameters.searchTerm =
                    searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                result = result.Where(s =>
                            (searchParameters.searchTerm == "") ||
                            s.Name.ToLower().Contains(searchParameters.searchTerm.ToLower()) ||
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

                List<TaskStatusModel> itemsModels =
                    CoreMapper.MapList<DB.Core.Models.TaskStatus, TaskStatusModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TaskStatusModel> GetById(int itemId)
        {
            DB.Core.Models.TaskStatus result = await _uow.TaskStatusRepo.GetById(itemId);

            TaskStatusModel itemModel =
                CoreMapper.MapObject<DB.Core.Models.TaskStatus, TaskStatusModel>(result);

            return itemModel;
        }

        public int Remove(TaskStatusModel itemModel)
        {
            DB.Core.Models.TaskStatus item = CoreMapper.MapObject<TaskStatusModel, DB.Core.Models.TaskStatus>(itemModel);
            item.IsActive = false;
            _uow.TaskStatusRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(TaskStatusModel itemModel)
        {
            DB.Core.Models.TaskStatus item = CoreMapper.MapObject<TaskStatusModel, DB.Core.Models.TaskStatus>(itemModel);
            item.IsActive = true;
            _uow.TaskStatusRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

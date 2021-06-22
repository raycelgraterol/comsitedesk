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
    public class ProjectStatusService : IProjectStatusService
    {
        public IUnitOfWork _uow;

        public ProjectStatusService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(ProjectStatusModel itemModel)
        {
            ProjectStatus _item = CoreMapper.MapObject<ProjectStatusModel, ProjectStatus>(itemModel);
            _item.IsActive = true;
            await _uow.ProjectStatusRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<ProjectStatusModel>> GetAllAsync()
        {
            List<ProjectStatusModel> items =
               CoreMapper.MapList<ProjectStatus, ProjectStatusModel>(await _uow.ProjectStatusRepo.GetAll().ToListAsync());

            return items;
        }

        public List<ProjectStatusModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.ProjectStatusRepo.GetAll();

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

                List<ProjectStatusModel> itemsModels =
                    CoreMapper.MapList<ProjectStatus, ProjectStatusModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ProjectStatusModel> GetById(int itemId)
        {
            ProjectStatus result = await _uow.ProjectStatusRepo.GetById(itemId);

            ProjectStatusModel itemModel =
                CoreMapper.MapObject<ProjectStatus, ProjectStatusModel>(result);

            return itemModel;
        }

        public int Remove(ProjectStatusModel itemModel)
        {
            ProjectStatus item = CoreMapper.MapObject<ProjectStatusModel, ProjectStatus>(itemModel);
            item.IsActive = false;
            _uow.ProjectStatusRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(ProjectStatusModel itemModel)
        {
            ProjectStatus item = CoreMapper.MapObject<ProjectStatusModel, ProjectStatus>(itemModel);
            item.IsActive = true;
            _uow.ProjectStatusRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

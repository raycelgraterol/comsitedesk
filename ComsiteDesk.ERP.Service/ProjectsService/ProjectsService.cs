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
    public class ProjectsService : IProjectsService
    {
        public IUnitOfWork _uow;

        public ProjectsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(ProjectModel itemModel)
        {
            Projects _item = CoreMapper.MapObject<ProjectModel, Projects>(itemModel);
            _item.IsActive = true;
            await _uow.ProjectsRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<ProjectModel>> GetAllAsync()
        {
            List<ProjectModel> items =
                CoreMapper.MapList<Projects, ProjectModel>(await _uow.ProjectsRepo.GetAll().ToListAsync());

            return items;
        }

        public List<ProjectModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.ProjectsRepo.GetAll()
                                .Include(x => x.ProjectStatus)
                                .Include(x => x.Organization)
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

                List<ProjectModel> itemsModels =
                    CoreMapper.MapList<Projects, ProjectModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ProjectModel> GetById(int itemId)
        {
            Projects result = await _uow.ProjectsRepo.GetById(itemId);

            ProjectModel itemModel =
                CoreMapper.MapObject<Projects, ProjectModel>(result);

            return itemModel;
        }

        public int Remove(ProjectModel itemModel)
        {
            Projects item = CoreMapper.MapObject<ProjectModel, Projects>(itemModel);
            item.IsActive = false;
            _uow.ProjectsRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(ProjectModel itemModel)
        {
            Projects item = CoreMapper.MapObject<ProjectModel, Projects>(itemModel);
            item.IsActive = true;
            _uow.ProjectsRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

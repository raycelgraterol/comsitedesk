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
            itemModel.Id = _item.Id;

            await InserProjectUsersAsync(itemModel);
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<ProjectModel>> GetAllAsync()
        {
            List<ProjectModel> items =
                CoreMapper.MapList<Projects, ProjectModel>(await _uow.ProjectsRepo.GetAll().ToListAsync());

            return items;
        }

        public async Task<List<ProjectUserModel>> GetAllUsersByTicket(int projectId)
        {
            var result = _uow.ProjectsUsersRepo.GetAll().Where(x => x.ProjectsId == projectId);

            List<ProjectUserModel> items =
                CoreMapper.MapList<ProjectsUsers, ProjectUserModel>(await result.ToListAsync());

            return items;
        }

        public List<ProjectModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.ProjectsRepo.GetAll()
                                .Include(x => x.ProjectStatus)
                                .Include(x => x.Organization)
                                .ToList()
                                .Select(x => 
                                {
                                    x.TotalTasks = _uow.TasksRepo.GetAll().Count(t => t.ProjectsId == x.Id);
                                    x.Users = _uow.ProjectsUsersRepo.GetAll().Include(u => u.User).Include(y => y.Projects)
                                                    .Where(p => p.ProjectsId == x.Id).ToList();
                                    return x;
                                })
                                .AsQueryable();

                if (searchParameters.parentId != 0)
                {
                    result = result.Where(s => s.ProjectStatusId == searchParameters.parentId);
                }

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
            Projects result = await _uow.ProjectsRepo.GetAll()
                                                .Include(x => x.ProjectStatus)
                                                .Include(x => x.Organization)
                                                .FirstOrDefaultAsync(x => x.Id == itemId);

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

        public async Task<int> Update(ProjectModel itemModel)
        {
            Projects item = CoreMapper.MapObject<ProjectModel, Projects>(itemModel);
            item.IsActive = true;
            _uow.ProjectsRepo.Edit(item);

            await InserProjectUsersAsync(itemModel);

            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        private async System.Threading.Tasks.Task InserProjectUsersAsync(ProjectModel itemModel)
        {
            try
            {
                itemModel.UsersIds = itemModel.UsersIds == null ? new long[0] : itemModel.UsersIds;

                var existingItems = _uow.ProjectsUsersRepo.GetAll()
                                        .Where(x => x.ProjectsId == itemModel.Id)
                                        .ToList();

                //Set is active false for all exist items 
                foreach (var currentItems in existingItems)
                {
                    _uow.ProjectsUsersRepo.Delete(currentItems);
                    _uow.Commit();
                }

                //Loop for all formAction ids that come from the user
                foreach (var userId in itemModel.UsersIds)
                {
                    //check if the item exist to update else it is created.
                    var item = _uow.ProjectsUsersRepo.GetAll()
                                        .FirstOrDefault(x => x.UserId == userId && x.ProjectsId == itemModel.Id);

                    if (item != null)
                    {
                        var projectsUsers = new ProjectsUsers()
                        {
                            ProjectsId = item.ProjectsId,
                            UserId = item.UserId,
                            IsActive = true
                        };

                        _uow.ProjectsUsersRepo.Edit(projectsUsers);
                        _uow.Commit();
                    }
                    else
                    {
                        var projectsUsers = new ProjectsUsers()
                        {
                            ProjectsId = itemModel.Id,
                            UserId = userId,
                            CreatedBy = itemModel.CreatedBy,
                            DateCreated = DateTime.Now,
                            IsActive = true
                        };

                        await _uow.ProjectsUsersRepo.Insert(projectsUsers);
                        _uow.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

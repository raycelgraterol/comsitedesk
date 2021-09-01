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
    public class RoleFormActionService : IRoleFormActionService
    {
        public IUnitOfWork _uow;

        public RoleFormActionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public bool CheckRoleModule(RoleFormActionModel itemModel)
        {   
            var result = from T1 in _uow.FormRepo.GetAll().Include(x => x.Module)
                         join T2 in _uow.FormActionRepo.GetAll().Include(x => x.Action).Include(x => x.Roles)
                             on T1.Id equals T2.FormId
                            where T1.Module.URI == itemModel.URIModule && T2.Roles.Any(x => x.RoleId == itemModel.RoleId) &&
                            T1.IsActive == true && T2.IsActive == true
                            select new { form = T1, formAction = T2 };

            return result.Count() > 0;
        }

        public bool CheckRoleCanViewForm(RoleFormActionModel itemModel)
        {
            try
            {
                if (itemModel.URIForm.Count(f => f == '/') >= 3)
                {
                    string[] subs = itemModel.URIForm.Split('/');

                    itemModel.URIForm = '/' + subs[1] + '/' + subs[2];
                }
                
                var result = from T1 in _uow.FormRepo.GetAll().Include(x => x.Module)
                             join T2 in _uow.FormActionRepo.GetAll().Include(x => x.Action).Include(x => x.Roles) on T1.Id equals T2.FormId
                             where T2.Roles.Any(x => x.RoleId == itemModel.RoleId) && T2.Action.Name == itemModel.ActionName
                             && (T1.URI == itemModel.URIForm || itemModel.URIForm == T1.Module.URI + T1.URI)
                             select new { form = T1, formAction = T2 };

                return result.Count() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public void Add(RoleFormActionModel itemModel)
        {
            RoleFormAction _item = CoreMapper.MapObject<RoleFormActionModel, RoleFormAction>(itemModel);
            _item.IsActive = true;
            _uow.RoleFormActionRepo.Insert(_item);
            _uow.Commit();
        }

        public void AddRange(RoleFormArrayAction items)
        {
            var existingRoleFormActions = _uow.RoleFormActionRepo.GetAllRaw()
                                            .Include(x => x.FormAction)
                                            .Where(x => x.RoleId == items.RoleId && x.FormAction.FormId == items.FormId)
                                            .ToList();

            //Set is active false for all exist role form actions 
            foreach (var currentformAction in existingRoleFormActions)
            {
                var itemModel = new RoleFormActionModel()
                {
                    FormActionId = currentformAction.FormActionId,
                    RoleId = currentformAction.RoleId
                };

                Remove(itemModel);
            }

            //Loop for all formAction ids that come from the user
            foreach (var formActionId in items.formActionIds)
            {
                //check if the item exist to update else it is created.
                var item = existingRoleFormActions.FirstOrDefault(x => x.FormActionId == formActionId);

                if (item != null)
                {
                    var itemModel = new RoleFormActionModel()
                    {
                        FormActionId = item.FormActionId,
                        RoleId = item.RoleId,
                        IsActive = true
                    };

                    Update(itemModel);                    
                }
                else
                {
                    var itemModel = new RoleFormActionModel()
                    {
                        FormActionId = formActionId,
                        RoleId = items.RoleId,
                        IsActive = true
                    };

                    Add(itemModel);
                }
                                
            }           

        }

        public List<RoleFormActionModel> GetAll()
        {
            List<RoleFormActionModel> items =
               CoreMapper.MapList<RoleFormAction, RoleFormActionModel>(_uow.RoleFormActionRepo.GetAll().ToList());

            return items;
        }

        public List<RoleFormActionModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = RolFormsActionsJoins().AsQueryable();

                if (searchParameters.parentId != 0)
                {
                    result = result.Where(x => x.RoleId == searchParameters.parentId);
                }

                //count all items
                searchParameters.totalCount = result.Count();

                searchParameters.searchTerm =
                    searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                result = result.Where(s =>
                            (searchParameters.searchTerm == "") ||
                            s.RoleId.ToString().Contains(searchParameters.searchTerm.ToLower()));

                if (searchParameters.sortColumn != null)
                {
                    //Sorting
                    result = result.AsQueryable()
                            .OrderBy(searchParameters.sortColumn + " " + searchParameters.sortDirection);
                }
                else
                {
                    result = result.OrderBy(x => x.RoleId);
                }

                result = result
                            .Skip(searchParameters.startIndex)
                            .Take(searchParameters.PageSize);

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RoleFormActionModel> GetById(long roleId, int formActionId)
        {
            RoleFormAction result = await _uow.RoleFormActionRepo.GetAll().FirstOrDefaultAsync(x => x.RoleId == roleId && x.FormActionId == formActionId);

            RoleFormActionModel itemModel =
                CoreMapper.MapObject<RoleFormAction, RoleFormActionModel>(result);

            return itemModel;
        }

        public void Remove(RoleFormActionModel itemModel)
        {
            RoleFormAction item = CoreMapper.MapObject<RoleFormActionModel, RoleFormAction>(itemModel);
            item.IsActive = false;
            _uow.RoleFormActionRepo.Edit(item);
            _uow.Commit();
        }

        public void Update(RoleFormActionModel itemModel)
        {
            RoleFormAction item = CoreMapper.MapObject<RoleFormActionModel, RoleFormAction>(itemModel);
            item.IsActive = true;
            _uow.RoleFormActionRepo.Edit(item);
            _uow.Commit();
        }

        private List<RoleFormActionModel> RolFormsActionsJoins()
        {
            try
            {
                var result = (from T1 in _uow.RoleFormActionRepo.GetAll().Include(x => x.FormAction).Include(x => x.Role)
                              join T2 in _uow.FormActionRepo.GetAll().Include(x => x.Form).Include(x => x.Action)
                                on T1.FormActionId equals T2.Id
                              join T3 in _uow.ModuleRepo.GetAll()
                                on T2.Form.ModuleId equals T3.Id
                              select new {
                                  RoleFormActions = T1,
                                  ModuleName = T3.Name,
                                  FormItem = T2.Form,
                                  FormActions = T2                                  
                              })
                             .ToList()
                             .GroupBy(
                                p => new { p.RoleFormActions.Role, p.FormItem, p.ModuleName },
                                p => p.FormActions.Action,
                                (key, g) => new RoleFormActionModel
                                {
                                    RoleId = key.Role.Id,
                                    RoleName = key.Role.Name,
                                    ModuleId = key.FormItem.ModuleId,
                                    ModuleName = key.ModuleName,
                                    FormId = key.FormItem.Id,
                                    FormName = key.FormItem.Name,
                                    ActionsName = g.Select(x => x.Name).ToList()
                                })
                             .ToList();

                return result;
            }
            catch (Exception ex)
            {
                return new List<RoleFormActionModel>();
            }
        }
    }
}

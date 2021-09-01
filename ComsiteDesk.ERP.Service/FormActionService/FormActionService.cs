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
    public class FormActionService : IFormActionService
    {
        public IUnitOfWork _uow;

        public FormActionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(FormActionModel itemModel)
        {
            FormAction _item = CoreMapper.MapObject<FormActionModel, FormAction>(itemModel);
            _item.IsActive = true;
            await _uow.FormActionRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public List<FormActionModel> GetAll()
        {
            var resultData = _uow.FormActionRepo.GetAll()
                                .Include(x => x.Form)
                                .Include(x => x.Action)
                                .ToList();

            List<FormActionModel> items =
               CoreMapper.MapList<FormAction, FormActionModel>(resultData);

            return items;
        }

        public List<FormActionModel> GetAllByRolAndForm(long RoleId, int FormId)
        {
            var resultData = ListFormsActionsByRol(RoleId, FormId);

            return resultData;
        }

        public List<FormActionModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.FormActionRepo.GetAll()
                                .Include(x => x.Form)
                                .Include(x => x.Action)
                                .AsQueryable();

                if (searchParameters.parentId != 0)
                {
                    result = result.Where(x => x.FormId == searchParameters.parentId);
                }

                //count all items
                searchParameters.totalCount = result.Count();

                searchParameters.searchTerm =
                    searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                result = result.Where(s =>
                            (searchParameters.searchTerm == "") ||
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

                List<FormActionModel> itemsModels =
                    CoreMapper.MapList<FormAction, FormActionModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<FormActionModel> GetById(int itemId)
        {
            FormAction result = await _uow.FormActionRepo.GetById(itemId);

            FormActionModel itemModel =
                CoreMapper.MapObject<FormAction, FormActionModel>(result);

            return itemModel;
        }

        public int Remove(FormActionModel itemModel)
        {
            FormAction item = CoreMapper.MapObject<FormActionModel, FormAction>(itemModel);
            item.IsActive = false;
            _uow.FormActionRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(FormActionModel itemModel)
        {
            FormAction item = CoreMapper.MapObject<FormActionModel, FormAction>(itemModel);
            item.IsActive = true;
            _uow.FormActionRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }


        private List<FormActionModel> ListFormsActionsByRol(long RoleId, int formId)
        {
            try
            {
                var result = (from T1 in _uow.RoleFormActionRepo.GetAll().Include(x => x.FormAction)
                              join T2 in _uow.FormActionRepo.GetAll().Include(x => x.Action)
                              on T1.FormActionId equals T2.Id
                              where T1.RoleId == RoleId && T2.FormId == formId
                              select new FormActionModel
                              {
                                  Id = T1.FormActionId,
                                  ActionId = T2.ActionId,
                                  ActionName = T2.Action.Name
                              })
                              .ToList();
                              

                return result;
            }
            catch (Exception ex)
            {
                return new List<FormActionModel>();
            }
        }
    }
}

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
using Action = ComsiteDesk.ERP.DB.Core.Models.Action;

namespace ComsiteDesk.ERP.Service
{
    public class ActionService : IActionService
    {
        public IUnitOfWork _uow;

        public ActionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(ActionModel itemModel)
        {
            Action _item = CoreMapper.MapObject<ActionModel, Action>(itemModel);
            _item.IsActive = true;
            await _uow.ActionRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public List<ActionModel> GetAll()
        {
            List<ActionModel> items =
               CoreMapper.MapList<Action, ActionModel>(_uow.ActionRepo.GetAll().ToList());

            return items;
        }

        public List<ActionModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.ActionRepo.GetAll();

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

                List<ActionModel> itemsModels =
                    CoreMapper.MapList<Action, ActionModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ActionModel> GetById(int itemId)
        {
            Action result = await _uow.ActionRepo.GetById(itemId);

            ActionModel itemModel =
                CoreMapper.MapObject<Action, ActionModel>(result);

            return itemModel;
        }

        public int Remove(ActionModel itemModel)
        {
            Action item = CoreMapper.MapObject<ActionModel, Action>(itemModel);
            item.IsActive = false;
            _uow.ActionRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(ActionModel itemModel)
        {
            Action item = CoreMapper.MapObject<ActionModel, Action>(itemModel);
            item.IsActive = true;
            _uow.ActionRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

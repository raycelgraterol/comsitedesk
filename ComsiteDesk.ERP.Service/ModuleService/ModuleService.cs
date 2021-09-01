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
    public class ModuleService : IModuleService
    {
        public IUnitOfWork _uow;

        public ModuleService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(ModuleModel itemModel)
        {
            Module _item = CoreMapper.MapObject<ModuleModel, Module>(itemModel);
            _item.IsActive = true;
            await _uow.ModuleRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public List<ModuleModel> GetAll()
        {
            List<ModuleModel> items =
               CoreMapper.MapList<Module, ModuleModel>(_uow.ModuleRepo.GetAll().ToList());

            return items;
        }

        public List<ModuleModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.ModuleRepo.GetAll();

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

                List<ModuleModel> itemsModels =
                    CoreMapper.MapList<Module, ModuleModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ModuleModel> GetById(int itemId)
        {
            Module result = await _uow.ModuleRepo.GetById(itemId);

            ModuleModel itemModel =
                CoreMapper.MapObject<Module, ModuleModel>(result);

            return itemModel;
        }

        public int Remove(ModuleModel itemModel)
        {
            Module item = CoreMapper.MapObject<ModuleModel, Module>(itemModel);
            item.IsActive = false;
            _uow.ModuleRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(ModuleModel itemModel)
        {
            Module item = CoreMapper.MapObject<ModuleModel, Module>(itemModel);
            item.IsActive = true;
            _uow.ModuleRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

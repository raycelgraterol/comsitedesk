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
    public class EquipmentService : IEquipmentService
    {
        public IUnitOfWork _uow;

        public EquipmentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(EquipmentModel itemModel)
        {
            Equipment _item = CoreMapper.MapObject<EquipmentModel, Equipment>(itemModel);
            _item.IsActive = true;
            await _uow.EquipmentRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<EquipmentModel>> GetAllAsync()
        {
            List<EquipmentModel> items =
                CoreMapper.MapList<Equipment, EquipmentModel>(await _uow.EquipmentRepo.GetAll().ToListAsync());

            return items;
        }

        public List<EquipmentModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.EquipmentRepo.GetAll();

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

                List<EquipmentModel> itemsModels =
                    CoreMapper.MapList<Equipment, EquipmentModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<EquipmentModel> GetById(int itemId)
        {
            Equipment result = await _uow.EquipmentRepo.GetById(itemId);

            EquipmentModel itemModel =
                CoreMapper.MapObject<Equipment, EquipmentModel>(result);

            return itemModel;
        }

        public int Remove(EquipmentModel itemModel)
        {
            Equipment item = CoreMapper.MapObject<EquipmentModel, Equipment>(itemModel);
            item.IsActive = false;
            _uow.EquipmentRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(EquipmentModel itemModel)
        {
            Equipment item = CoreMapper.MapObject<EquipmentModel, Equipment>(itemModel);
            item.IsActive = true;
            _uow.EquipmentRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

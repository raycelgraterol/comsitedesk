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
    public class EquipmentUserService : IEquipmentUserService
    {
        public IUnitOfWork _uow;

        public EquipmentUserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(EquipmentUserModel itemModel)
        {
            EquipmentUser _item = CoreMapper.MapObject<EquipmentUserModel, EquipmentUser>(itemModel);
            _item.IsActive = true;
            await _uow.EquipmentUserRepo.Insert(_item);
            _uow.Commit();
            itemModel.Id = _item.Id;

            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<EquipmentUserModel>> GetAllAsync()
        {
            List<EquipmentUserModel> items =
                CoreMapper.MapList<EquipmentUser, EquipmentUserModel>(await _uow.EquipmentUserRepo.GetAll().ToListAsync());

            return items;
        }

        public List<EquipmentUserModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                IQueryable<EquipmentUser> result = _uow.EquipmentUserRepo.GetAll();

                //count all items
                searchParameters.totalCount = result.Count();

                searchParameters.searchTerm =
                    searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                result = result.Where(s =>
                            (searchParameters.searchTerm == "") ||
                            s.Name.ToLower().Contains(searchParameters.searchTerm.ToLower()));

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

                List<EquipmentUserModel> itemsModels =
                    CoreMapper.MapList<EquipmentUser, EquipmentUserModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<EquipmentUserModel> GetById(int itemId)
        {
            EquipmentUser result = await _uow.EquipmentUserRepo.GetById(itemId);

            EquipmentUserModel itemModel =
                CoreMapper.MapObject<EquipmentUser, EquipmentUserModel>(result);

            return itemModel;
        }

        public int Remove(EquipmentUserModel itemModel)
        {
            EquipmentUser item = CoreMapper.MapObject<EquipmentUserModel, EquipmentUser>(itemModel);
            item.IsActive = false;
            _uow.EquipmentUserRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(EquipmentUserModel itemModel)
        {
            EquipmentUser item = CoreMapper.MapObject<EquipmentUserModel, EquipmentUser>(itemModel);

            item.IsActive = true;
            _uow.EquipmentUserRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

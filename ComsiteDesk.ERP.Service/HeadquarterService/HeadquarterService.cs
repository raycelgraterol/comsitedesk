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
    public class HeadquarterService : IHeadquarterService
    {
        public IUnitOfWork _uow;

        public HeadquarterService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(HeadquarterModel itemModel)
        {
            Headquarter _item = CoreMapper.MapObject<HeadquarterModel, Headquarter>(itemModel);
            _item.IsActive = true;
            await _uow.HeadquarterRepo.Insert(_item);
            _uow.Commit();
            itemModel.Id = _item.Id;

            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<HeadquarterModel>> GetAllAsync()
        {
            List<HeadquarterModel> items =
                CoreMapper.MapList<Headquarter, HeadquarterModel>(await _uow.HeadquarterRepo.GetAll().ToListAsync());

            return items;
        }

        public List<HeadquarterModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                IQueryable<Headquarter> result = _uow.HeadquarterRepo.GetAll();

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

                List<HeadquarterModel> itemsModels =
                    CoreMapper.MapList<Headquarter, HeadquarterModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<HeadquarterModel> GetById(int itemId)
        {
            Headquarter result = await _uow.HeadquarterRepo.GetById(itemId);

            HeadquarterModel itemModel =
                CoreMapper.MapObject<Headquarter, HeadquarterModel>(result);

            return itemModel;
        }

        public int Remove(HeadquarterModel itemModel)
        {
            Headquarter item = CoreMapper.MapObject<HeadquarterModel, Headquarter>(itemModel);
            item.IsActive = false;
            _uow.HeadquarterRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(HeadquarterModel itemModel)
        {
            Headquarter item = CoreMapper.MapObject<HeadquarterModel, Headquarter>(itemModel);

            item.IsActive = true;
            _uow.HeadquarterRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

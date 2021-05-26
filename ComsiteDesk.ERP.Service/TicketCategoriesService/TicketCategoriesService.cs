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

namespace ComsiteDesk.ERP.Service.TicketCategoriesService
{
    public class TicketCategoriesService : ITicketCategoriesService
    {
        public IUnitOfWork _uow;

        public TicketCategoriesService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(TicketCategoriesModel itemModel)
        {
            TicketCategories _item = CoreMapper.MapObject<TicketCategoriesModel, TicketCategories>(itemModel);
            _item.IsActive = true;
            await _uow.TicketCategoriesRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<TicketCategoriesModel>> GetAllAsync()
        {
            List<TicketCategoriesModel> items =
                CoreMapper.MapList<TicketCategories, TicketCategoriesModel>(await _uow.TicketCategoriesRepo.GetAll().ToListAsync());

            return items;
        }

        public List<TicketCategoriesModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.TicketCategoriesRepo.GetAll();

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

                List<TicketCategoriesModel> itemsModels =
                    CoreMapper.MapList<TicketCategories, TicketCategoriesModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TicketCategoriesModel> GetById(int itemId)
        {
            TicketCategories result = await _uow.TicketCategoriesRepo.GetById(itemId);

            TicketCategoriesModel itemModel =
                CoreMapper.MapObject<TicketCategories, TicketCategoriesModel>(result);

            return itemModel;
        }

        public int Remove(TicketCategoriesModel itemModel)
        {
            TicketCategories item = CoreMapper.MapObject<TicketCategoriesModel, TicketCategories>(itemModel);
            item.IsActive = false;
            _uow.TicketCategoriesRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(TicketCategoriesModel itemModel)
        {
            TicketCategories item = CoreMapper.MapObject<TicketCategoriesModel, TicketCategories>(itemModel);
            item.IsActive = true;
            _uow.TicketCategoriesRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

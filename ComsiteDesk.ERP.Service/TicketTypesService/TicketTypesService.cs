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
    public class TicketTypesService : ITicketTypesService
    {
        public IUnitOfWork _uow;

        public TicketTypesService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(TicketTypesModel itemModel)
        {
            TicketTypes _item = CoreMapper.MapObject<TicketTypesModel, TicketTypes>(itemModel);
            _item.IsActive = true;
            await _uow.TicketTypesRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<TicketTypesModel>> GetAllAsync()
        {
            List<TicketTypesModel> items =
                CoreMapper.MapList<TicketTypes, TicketTypesModel>(await _uow.TicketTypesRepo.GetAll().ToListAsync());

            return items;
        }

        public List<TicketTypesModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.TicketTypesRepo.GetAll();

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

                List<TicketTypesModel> itemsModels =
                    CoreMapper.MapList<TicketTypes, TicketTypesModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TicketTypesModel> GetById(int itemId)
        {
            TicketTypes result = await _uow.TicketTypesRepo.GetById(itemId);

            TicketTypesModel itemModel =
                CoreMapper.MapObject<TicketTypes, TicketTypesModel>(result);

            return itemModel;
        }

        public int Remove(TicketTypesModel itemModel)
        {
            TicketTypes item = CoreMapper.MapObject<TicketTypesModel, TicketTypes>(itemModel);
            item.IsActive = false;
            _uow.TicketTypesRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(TicketTypesModel itemModel)
        {
            TicketTypes item = CoreMapper.MapObject<TicketTypesModel, TicketTypes>(itemModel);
            item.IsActive = true;
            _uow.TicketTypesRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

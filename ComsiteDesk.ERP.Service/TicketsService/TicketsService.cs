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
    public class TicketsService : ITicketsService
    {
        public IUnitOfWork _uow;

        public TicketsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(TicketModel itemModel)
        {
            Tickets _item = CoreMapper.MapObject<TicketModel, Tickets>(itemModel);
            _item.IsActive = true;
            await _uow.TicketsRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<TicketModel>> GetAllAsync()
        {
            List<TicketModel> items =
                CoreMapper.MapList<Tickets, TicketModel>(await _uow.TicketsRepo.GetAll().ToListAsync());

            return items;
        }

        public List<TicketModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.TicketsRepo.GetAll();

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

                List<TicketModel> itemsModels =
                    CoreMapper.MapList<Tickets, TicketModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TicketModel> GetById(int itemId)
        {
            Tickets result = await _uow.TicketsRepo.GetById(itemId);

            TicketModel itemModel =
                CoreMapper.MapObject<Tickets, TicketModel>(result);

            return itemModel;
        }

        public int Remove(TicketModel itemModel)
        {
            Tickets item = CoreMapper.MapObject<TicketModel, Tickets>(itemModel);
            item.IsActive = false;
            _uow.TicketsRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(TicketModel itemModel)
        {
            Tickets item = CoreMapper.MapObject<TicketModel, Tickets>(itemModel);
            item.IsActive = true;
            _uow.TicketsRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

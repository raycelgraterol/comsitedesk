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
    public class TicketStatusService : ITicketStatusService
    {
        public IUnitOfWork _uow;

        public TicketStatusService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(TicketStatusModel itemModel)
        {
            TicketStatus _item = CoreMapper.MapObject<TicketStatusModel, TicketStatus>(itemModel);
            _item.IsActive = true;
            await _uow.TicketStatusRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<TicketStatusModel>> GetAllAsync()
        {
            List<TicketStatusModel> items =
                CoreMapper.MapList<TicketStatus, TicketStatusModel>(await _uow.TicketStatusRepo.GetAll().ToListAsync());

            return items;
        }

        public List<TicketStatusModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.TicketStatusRepo.GetAll();

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

                List<TicketStatusModel> itemsModels =
                    CoreMapper.MapList<TicketStatus, TicketStatusModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TicketStatusModel> GetById(int itemId)
        {
            TicketStatus result = await _uow.TicketStatusRepo.GetById(itemId);

            TicketStatusModel itemModel =
                CoreMapper.MapObject<TicketStatus, TicketStatusModel>(result);

            return itemModel;
        }

        public int Remove(TicketStatusModel itemModel)
        {
            TicketStatus item = CoreMapper.MapObject<TicketStatusModel, TicketStatus>(itemModel);
            item.IsActive = false;
            _uow.TicketStatusRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(TicketStatusModel itemModel)
        {
            TicketStatus item = CoreMapper.MapObject<TicketStatusModel, TicketStatus>(itemModel);
            item.IsActive = true;
            _uow.TicketStatusRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

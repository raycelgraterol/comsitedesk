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

namespace ComsiteDesk.ERP.Service.TicketProcessesService
{
    public class TicketProcessesService : ITicketProcessesService
    {
        public IUnitOfWork _uow;

        public TicketProcessesService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(TicketProcessesModel itemModel)
        {
            TicketProcesses _item = CoreMapper.MapObject<TicketProcessesModel, TicketProcesses>(itemModel);
            _item.IsActive = true;
            await _uow.TicketProcessesRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<TicketProcessesModel>> GetAllAsync()
        {
            List<TicketProcessesModel> items =
                CoreMapper.MapList<TicketProcesses, TicketProcessesModel>(await _uow.TicketProcessesRepo.GetAll().ToListAsync());

            return items;
        }

        public List<TicketProcessesModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.TicketProcessesRepo.GetAll();

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

                List<TicketProcessesModel> itemsModels =
                    CoreMapper.MapList<TicketProcesses, TicketProcessesModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TicketProcessesModel> GetById(int itemId)
        {
            TicketProcesses result = await _uow.TicketProcessesRepo.GetById(itemId);

            TicketProcessesModel itemModel =
                CoreMapper.MapObject<TicketProcesses, TicketProcessesModel>(result);

            return itemModel;
        }

        public int Remove(TicketProcessesModel itemModel)
        {
            TicketProcesses item = CoreMapper.MapObject<TicketProcessesModel, TicketProcesses>(itemModel);
            item.IsActive = false;
            _uow.TicketProcessesRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(TicketProcessesModel itemModel)
        {
            TicketProcesses item = CoreMapper.MapObject<TicketProcessesModel, TicketProcesses>(itemModel);
            item.IsActive = true;
            _uow.TicketProcessesRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

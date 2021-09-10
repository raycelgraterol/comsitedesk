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
    public class ClientService: IClientService
    {
        public IUnitOfWork _uow;

        public ClientService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(ClientModel itemModel)
        {
            try
            {
                Client _item = CoreMapper.MapObject<ClientModel, Client>(itemModel);
                _item.IsActive = true;
                await _uow.ClientRepo.Insert(_item);
                _uow.Commit();
                return Convert.ToInt32(_item.Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ClientModel>> GetAllAsync()
        {
            List<ClientModel> items =
                CoreMapper.MapList<Client, ClientModel>(await _uow.ClientRepo.GetAll().ToListAsync());

            return items;
        }

        public List<ClientModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.ClientRepo.GetAll();

                //count all items
                searchParameters.totalCount = result.Count();

                searchParameters.searchTerm =
                    searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                result = result.Where(s =>
                            (searchParameters.searchTerm == "") ||
                            s.BusinessName.ToLower().Contains(searchParameters.searchTerm.ToLower()) ||
                            s.FirstName.ToLower().Contains(searchParameters.searchTerm.ToLower()) ||
                            s.LastName.ToLower().Contains(searchParameters.searchTerm.ToLower()) ||
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

                List<ClientModel> itemsModels =
                    CoreMapper.MapList<Client, ClientModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ClientModel> GetById(int itemId)
        {
            Client result = await _uow.ClientRepo.GetById(itemId);

            ClientModel itemModel =
                CoreMapper.MapObject<Client, ClientModel>(result);

            return itemModel;
        }

        public int Remove(ClientModel itemModel)
        {
            Client item = CoreMapper.MapObject<ClientModel, Client>(itemModel);
            item.IsActive = false;
            _uow.ClientRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(ClientModel itemModel)
        {
            Client item = CoreMapper.MapObject<ClientModel, Client>(itemModel);
            item.IsActive = true;
            _uow.ClientRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

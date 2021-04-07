using ComsiteDesk.ERP.DB.Core.Models;
using ComsiteDesk.ERP.Service.HelperModel;
using ComsiteDesk.ERP.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public class ClientsService : IClientsService
    {
        public IUnitOfWork _uow;

        public ClientsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<ClientModel> GetAll()
        {            
            List<ClientModel> clients =
                CoreMapper.MapList<Clients, ClientModel>(_uow.ClientRepo.GetAll().ToList());

            return clients;
        }

        public async Task<ClientModel> GetById(int ClientId)
        {
            var result = await _uow.ClientRepo.GetById(ClientId);

            ClientModel client =
                CoreMapper.MapObject<Clients, ClientModel>(result);

            return client;
        }

        public async Task<int> Add(ClientModel Client)
        {
            Clients c = CoreMapper.MapObject<ClientModel, Clients>(Client);
            c.IsActive = true;
            await _uow.ClientRepo.Insert(c);
            _uow.Commit();
            return Convert.ToInt32(c.Id);
        }

        public int Update(ClientModel Client)
        {
            Clients c = CoreMapper.MapObject<ClientModel, Clients>(Client);
            c.IsActive = true;
            _uow.ClientRepo.Edit(c);
            _uow.Commit();
            return Convert.ToInt32(c.Id);
        }

        public int Remove(ClientModel Client)
        {
            Clients c = CoreMapper.MapObject<ClientModel, Clients>(Client);
            c.IsActive = false;
            _uow.ClientRepo.Edit(c);
            _uow.Commit();
            return Convert.ToInt32(c.Id);
        }

        public List<ClientModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.ClientRepo.GetAll();

                //count all items
                searchParameters.CountItems = result.Count();

                searchParameters.searchTerm =
                    searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                result = result.Where(s =>
                            (searchParameters.searchTerm == "") ||
                            s.BusinessName.Contains(searchParameters.searchTerm) ||
                            s.RIF.ToLower().Contains(searchParameters.searchTerm.ToLower()));

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

                List<ClientModel> clientModels =
                    CoreMapper.MapList<Clients, ClientModel>(result.ToList());

                return clientModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

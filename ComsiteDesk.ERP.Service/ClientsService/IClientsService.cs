using ComsiteDesk.ERP.DB.Core.Models;
using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IClientsService
    {
        List<ClientModel> GetAll();
        List<ClientModel> GetAllWithPager(SearchParameters searchParameters);
        Task<ClientModel> GetById(int ClientId);
        Task<int> Add(ClientModel Client);
        int Update(ClientModel Client);
        int Remove(ClientModel Client);
    }
}

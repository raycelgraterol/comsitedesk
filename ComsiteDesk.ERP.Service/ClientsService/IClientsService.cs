using ComsiteDesk.ERP.DB.Core.Models;
using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service
{
    public interface IClientsService
    {
        List<ClientModel> GetAll();
        List<ClientModel> GetAllWithPager(SearchParameters searchParameters);
        ClientModel GetById(int ClientId);
        int Add(ClientModel Client);
        int Update(ClientModel Client);
        int Remove(ClientModel Client);
    }
}

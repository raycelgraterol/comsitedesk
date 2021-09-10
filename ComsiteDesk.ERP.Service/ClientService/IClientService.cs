using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IClientService
    {
        Task<int> Add(ClientModel itemModel);
        int Update(ClientModel itemModel);
        Task<List<ClientModel>> GetAllAsync();
        List<ClientModel> GetAllWithPager(SearchParameters searchParameters);
        Task<ClientModel> GetById(int itemId);
        int Remove(ClientModel itemModel);
    }
}

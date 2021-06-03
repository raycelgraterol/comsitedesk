using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface ITicketTypesService
    {
        Task<int> Add(TicketTypesModel itemModel);
        int Update(TicketTypesModel itemModel);
        Task<List<TicketTypesModel>> GetAllAsync();
        List<TicketTypesModel> GetAllWithPager(SearchParameters searchParameters);
        Task<TicketTypesModel> GetById(int itemId);
        int Remove(TicketTypesModel itemModel);        
    }
}

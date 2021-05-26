using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service.TicketStatusService
{
    public interface ITicketStatusService
    {
        Task<int> Add(TicketStatusModel itemModel);
        int Update(TicketStatusModel itemModel);
        Task<List<TicketStatusModel>> GetAllAsync();
        List<TicketStatusModel> GetAllWithPager(SearchParameters searchParameters);
        Task<TicketStatusModel> GetById(int itemId);
        int Remove(TicketStatusModel itemModel);
    }
}

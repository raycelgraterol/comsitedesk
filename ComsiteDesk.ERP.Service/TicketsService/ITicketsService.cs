using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface ITicketsService
    {
        Task<int> Add(TicketModel itemModel);
        int Update(TicketModel itemModel);
        Task<List<TicketModel>> GetAllAsync();
        List<TicketModel> GetAllWithPager(SearchParameters searchParameters);
        Task<TicketModel> GetById(int itemId);
        int Remove(TicketModel itemModel);
    }
}

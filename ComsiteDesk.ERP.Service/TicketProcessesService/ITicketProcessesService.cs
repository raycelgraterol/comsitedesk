using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service.TicketProcessesService
{
    public interface ITicketProcessesService
    {
        Task<int> Add(TicketProcessesModel itemModel);
        int Update(TicketProcessesModel itemModel);
        Task<List<TicketProcessesModel>> GetAllAsync();
        List<TicketProcessesModel> GetAllWithPager(SearchParameters searchParameters);
        Task<TicketProcessesModel> GetById(int itemId);
        int Remove(TicketProcessesModel itemModel);
    }
}

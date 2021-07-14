using ComsiteDesk.ERP.DB.Core.Models;
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
        Task<int> Update(TicketModel itemModel);
        List<TicketsBalancesModel> GetBalances();
        Task<List<TicketModel>> GetAllAsync();
        List<TicketModel> GetAllWithPager(TicketsSearchModel searchParameters);
        Task<TicketModel> GetById(int itemId);
        Task<List<TicketUserModel>> GetAllUsersByTicket(int TicketId);
        int Remove(TicketModel itemModel);
    }
}

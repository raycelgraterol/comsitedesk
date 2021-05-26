using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface ITicketCategoriesService
    {
        Task<int> Add(TicketCategoriesModel itemModel);
        int Update(TicketCategoriesModel itemModel);
        Task<List<TicketCategoriesModel>> GetAllAsync();
        List<TicketCategoriesModel> GetAllWithPager(SearchParameters searchParameters);
        Task<TicketCategoriesModel> GetById(int itemId);
        int Remove(TicketCategoriesModel itemModel);
    }
}

using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IActionService
    {
        Task<int> Add(ActionModel itemModel);
        int Update(ActionModel itemModel);
        List<ActionModel> GetAll();
        List<ActionModel> GetAllWithPager(SearchParameters searchParameters);
        Task<ActionModel> GetById(int itemId);
        int Remove(ActionModel itemModel);
    }
}

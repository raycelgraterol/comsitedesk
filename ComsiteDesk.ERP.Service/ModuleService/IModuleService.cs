using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IModuleService
    {
        Task<int> Add(ModuleModel itemModel);
        int Update(ModuleModel itemModel);
        List<ModuleModel> GetAll();
        List<ModuleModel> GetAllWithPager(SearchParameters searchParameters);
        Task<ModuleModel> GetById(int itemId);
        int Remove(ModuleModel itemModel);
    }
}

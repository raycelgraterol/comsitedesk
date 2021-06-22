using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IProjectStatusService
    {
        Task<int> Add(ProjectStatusModel itemModel);
        int Update(ProjectStatusModel itemModel);
        Task<List<ProjectStatusModel>> GetAllAsync();
        List<ProjectStatusModel> GetAllWithPager(SearchParameters searchParameters);
        Task<ProjectStatusModel> GetById(int itemId);
        int Remove(ProjectStatusModel itemModel);
    }
}

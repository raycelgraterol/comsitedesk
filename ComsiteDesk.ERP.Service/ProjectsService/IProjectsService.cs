using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IProjectsService
    {
        Task<int> Add(ProjectModel itemModel);
        Task<int> Update(ProjectModel itemModel);
        Task<List<ProjectModel>> GetAllAsync();
        Task<List<ProjectUserModel>> GetAllUsersByTicket(int projectId);
        List<ProjectModel> GetAllWithPager(SearchParameters searchParameters);
        Task<ProjectModel> GetById(int itemId);
        int Remove(ProjectModel itemModel);
    }
}

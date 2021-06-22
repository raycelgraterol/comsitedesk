using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface ITaskStatusService
    {
        Task<int> Add(TaskStatusModel itemModel);
        int Update(TaskStatusModel itemModel);
        Task<List<TaskStatusModel>> GetAllAsync();
        List<TaskStatusModel> GetAllWithPager(SearchParameters searchParameters);
        Task<TaskStatusModel> GetById(int itemId);
        int Remove(TaskStatusModel itemModel);
    }
}

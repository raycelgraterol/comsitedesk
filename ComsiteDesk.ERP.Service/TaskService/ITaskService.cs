using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface ITaskService
    {
        Task<int> Add(TaskModel itemModel);
        int Update(TaskModel itemModel);
        Task<List<TaskModel>> GetAllAsync();
        List<TaskModel> GetAllWithPager(SearchParameters searchParameters);
        Task<TaskModel> GetById(int itemId);
        int Remove(TaskModel itemModel);
    }
}

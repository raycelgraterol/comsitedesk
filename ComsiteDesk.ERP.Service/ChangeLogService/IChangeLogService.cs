using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IChangeLogService
    {
        Task<int> Add(ChangeLogModel itemModel);
        int Update(ChangeLogModel itemModel);
        Task<List<ChangeLogModel>> GetAllAsync();
        Task<ChangeLogModel> GetById(int itemId);
        int Remove(ChangeLogModel itemModel);
    }
}

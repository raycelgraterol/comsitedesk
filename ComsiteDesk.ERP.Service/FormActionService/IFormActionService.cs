using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IFormActionService
    {
        Task<int> Add(FormActionModel itemModel);
        int Update(FormActionModel itemModel);
        List<FormActionModel> GetAll();
        List<FormActionModel> GetAllByRolAndForm(long RoleId, int FormId);
        List<FormActionModel> GetAllWithPager(SearchParameters searchParameters);
        Task<FormActionModel> GetById(int itemId);
        int Remove(FormActionModel itemModel);
    }
}

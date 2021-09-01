using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IFormService
    {
        Task<int> Add(FormModel itemModel);
        bool FormExists(string URI);
        int Update(FormModel itemModel);
        List<FormModel> GetAll();
        List<FormModel> GetAllWithPager(SearchParameters searchParameters);
        Task<FormModel> GetById(int itemId);
        int Remove(FormModel itemModel);
    }
}

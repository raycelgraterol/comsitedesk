using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IDepartmentService
    {
        Task<int> Add(DepartmentModel itemModel);
        int Update(DepartmentModel itemModel);
        Task<List<DepartmentModel>> GetAllAsync();
        List<DepartmentModel> GetAllWithPager(SearchParameters searchParameters);
        Task<DepartmentModel> GetById(int itemId);
        int Remove(DepartmentModel itemModel);
    }
}

using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IEquipmentService
    {
        Task<int> Add(EquipmentModel itemModel);
        int Update(EquipmentModel itemModel);
        Task<List<EquipmentModel>> GetAllAsync();
        List<EquipmentModel> GetAllWithPager(SearchParameters searchParameters);
        Task<EquipmentModel> GetById(int itemId);
        int Remove(EquipmentModel itemModel);
    }
}

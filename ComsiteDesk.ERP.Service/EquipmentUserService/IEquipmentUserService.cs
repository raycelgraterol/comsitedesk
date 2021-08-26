using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IEquipmentUserService
    {
        Task<int> Add(EquipmentUserModel itemModel);
        int Update(EquipmentUserModel itemModel);
        Task<List<EquipmentUserModel>> GetAllAsync();
        List<EquipmentUserModel> GetAllWithPager(SearchParameters searchParameters);
        Task<EquipmentUserModel> GetById(int itemId);
        int Remove(EquipmentUserModel itemModel);
    }
}

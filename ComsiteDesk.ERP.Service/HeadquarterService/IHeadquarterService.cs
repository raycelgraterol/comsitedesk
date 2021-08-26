using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IHeadquarterService
    {
        Task<int> Add(HeadquarterModel itemModel);
        int Update(HeadquarterModel itemModel);
        Task<List<HeadquarterModel>> GetAllAsync();
        List<HeadquarterModel> GetAllWithPager(SearchParameters searchParameters);
        Task<HeadquarterModel> GetById(int itemId);
        int Remove(HeadquarterModel itemModel);
    }
}

using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IRoleFormActionService
    {
        bool CheckRoleModule(RoleFormActionModel itemModel);
        bool CheckRoleCanViewForm(RoleFormActionModel itemModel);
        void Add(RoleFormActionModel itemModel);
        void AddRange(RoleFormArrayAction items);
        void Update(RoleFormActionModel itemModel);
        List<RoleFormActionModel> GetAll();
        List<RoleFormActionModel> GetAllWithPager(SearchParameters searchParameters);
        Task<RoleFormActionModel> GetById(long roleId, int formActionId);
        void Remove(RoleFormActionModel itemModel);
    }
}

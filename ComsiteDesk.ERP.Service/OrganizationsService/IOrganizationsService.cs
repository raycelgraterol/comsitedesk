using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IOrganizationsService
    {
        List<OrganizationModel> GetAll();
        Task<OrganizationModel> GetById(int organizationId);
        Task<int> Add(OrganizationModel organization);
        int Update(OrganizationModel organization);
        int Remove(OrganizationModel organization);
        List<OrganizationModel> GetAllWithPager(SearchParameters searchParameters);
    }
}

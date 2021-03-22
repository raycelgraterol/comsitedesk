using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service
{
    public interface IOrganizationsService
    {
        List<OrganizationModel> GetAll();        
        OrganizationModel GetById(int ClientId);
        int Add(OrganizationModel Client);
        int Update(OrganizationModel Client);
        int Remove(OrganizationModel Client);
    }
}

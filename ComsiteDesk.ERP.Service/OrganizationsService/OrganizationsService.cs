using ComsiteDesk.ERP.DB.Core.Models;
using ComsiteDesk.ERP.Service.HelperModel;
using ComsiteDesk.ERP.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public class OrganizationsService : IOrganizationsService
    {
        public IUnitOfWork _uow;

        public OrganizationsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<OrganizationModel> GetAll()
        {
            List<OrganizationModel> Organizations =
                CoreMapper.MapList<Organizations, OrganizationModel>(_uow.OrganizationsRepo.GetAll().ToList());

            return Organizations;
        }

        public async Task<OrganizationModel> GetById(int organizationId)
        {
            var result = await _uow.OrganizationsRepo.GetById(organizationId);

            OrganizationModel organization =
                CoreMapper.MapObject<Organizations, OrganizationModel>(result);

            return organization;
        }

        public OrganizationModel GetMainOrganization()
        {
            var result = _uow.OrganizationsRepo.GetAll().FirstOrDefault(x => x.OrganizationTypesId == 1);

            OrganizationModel organization =
                CoreMapper.MapObject<Organizations, OrganizationModel>(result);

            return organization;
        }

        public async Task<int> Add(OrganizationModel organization)
        {
            Organizations o = CoreMapper.MapObject<OrganizationModel, Organizations>(organization);
            o.IsActive = true;
            await _uow.OrganizationsRepo.Insert(o);
            _uow.Commit();
            return Convert.ToInt32(o.Id);
        }

        public int Update(OrganizationModel organization)
        {
            Organizations o = CoreMapper.MapObject<OrganizationModel, Organizations>(organization);
            o.IsActive = true;
            _uow.OrganizationsRepo.Edit(o);
            _uow.Commit();
            return Convert.ToInt32(o.Id);
        }

        public int Remove(OrganizationModel organization)
        {
            Organizations o = CoreMapper.MapObject<OrganizationModel, Organizations>(organization);
            o.IsActive = false;
            _uow.OrganizationsRepo.Edit(o);
            _uow.Commit();
            return Convert.ToInt32(o.Id);
        }

        public List<OrganizationModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.OrganizationsRepo.GetAll();

                //count all items
                searchParameters.totalCount = result.Count();

                searchParameters.searchTerm =
                    searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                result = result.Where(s =>
                            (searchParameters.searchTerm == "") ||
                            s.BusinessName.Contains(searchParameters.searchTerm) ||
                            s.RIF.ToLower().Contains(searchParameters.searchTerm.ToLower()));

                if (searchParameters.sortColumn != null)
                {
                    //Sorting
                    result = result.AsQueryable()
                            .OrderBy(searchParameters.sortColumn + " " + searchParameters.sortDirection);
                }
                else
                {
                    result = result.OrderBy(x => x.Id);
                }

                result = result
                            .Skip(searchParameters.startIndex)
                            .Take(searchParameters.PageSize);

                List<OrganizationModel> OrganizationModels =
                    CoreMapper.MapList<Organizations, OrganizationModel>(result.ToList());

                return OrganizationModels;
            }
            catch (Exception ex)
            {
                return new List<OrganizationModel>();
            }
        }
    }
}

using ComsiteDesk.ERP.DB.Core.Models;
using ComsiteDesk.ERP.Service.HelperModel;
using ComsiteDesk.ERP.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

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

        public OrganizationModel GetById(int ClientId)
        {
            var result = _uow.OrganizationsRepo.GetById(ClientId);

            OrganizationModel client =
                CoreMapper.MapObject<Organizations, OrganizationModel>(result);

            return client;
        }

        public int Add(OrganizationModel Client)
        {
            Organizations o = CoreMapper.MapObject<OrganizationModel, Organizations>(Client);
            _uow.OrganizationsRepo.Insert(o);
            _uow.Commit();
            return Convert.ToInt32(o.Id);
        }

        public int Update(OrganizationModel Client)
        {
            Organizations c = CoreMapper.MapObject<OrganizationModel, Organizations>(Client);
            _uow.OrganizationsRepo.Edit(c);
            _uow.Commit();
            return Convert.ToInt32(c.Id);
        }

        public int Remove(OrganizationModel Client)
        {
            Organizations o = CoreMapper.MapObject<OrganizationModel, Organizations>(Client);
            _uow.OrganizationsRepo.Delete(o);
            _uow.Commit();
            return Convert.ToInt32(o.Id);
        }

        public List<OrganizationModel> GetAllWithPager(SearchParameters searchParameters, out int count)
        {
            try
            {
                var result = _uow.OrganizationsRepo.GetAll();

                //count all items
                count = result.Count();

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
                throw;
            }
        }
    }
}

using ComsiteDesk.ERP.DB.Core.Models;
using ComsiteDesk.ERP.Service.HelperModel;
using ComsiteDesk.ERP.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public class DepartmentService : IDepartmentService
    {
        public IUnitOfWork _uow;

        public DepartmentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(DepartmentModel itemModel)
        {
            Department _item = CoreMapper.MapObject<DepartmentModel, Department>(itemModel);
            _item.IsActive = true;
            await _uow.DepartmentRepo.Insert(_item);
            _uow.Commit();
            itemModel.Id = _item.Id;

            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<DepartmentModel>> GetAllAsync()
        {
            List<DepartmentModel> items =
                CoreMapper.MapList<Department, DepartmentModel>(await _uow.DepartmentRepo.GetAll().ToListAsync());

            return items;
        }

        public List<DepartmentModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                IQueryable<Department> result = _uow.DepartmentRepo.GetAll();

                //count all items
                searchParameters.totalCount = result.Count();

                searchParameters.searchTerm =
                    searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                result = result.Where(s =>
                            (searchParameters.searchTerm == "") ||
                            s.Name.ToLower().Contains(searchParameters.searchTerm.ToLower()));

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

                List<DepartmentModel> itemsModels =
                    CoreMapper.MapList<Department, DepartmentModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DepartmentModel> GetById(int itemId)
        {
            Department result = await _uow.DepartmentRepo.GetById(itemId);

            DepartmentModel itemModel =
                CoreMapper.MapObject<Department, DepartmentModel>(result);

            return itemModel;
        }

        public int Remove(DepartmentModel itemModel)
        {
            Department item = CoreMapper.MapObject<DepartmentModel, Department>(itemModel);
            item.IsActive = false;
            _uow.DepartmentRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(DepartmentModel itemModel)
        {
            Department item = CoreMapper.MapObject<DepartmentModel, Department>(itemModel);

            item.IsActive = true;
            _uow.DepartmentRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

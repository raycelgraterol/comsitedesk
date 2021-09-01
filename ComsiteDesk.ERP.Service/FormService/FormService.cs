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
    public class FormService : IFormService
    {
        public IUnitOfWork _uow;

        public FormService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(FormModel itemModel)
        {
            Form _item = CoreMapper.MapObject<FormModel, Form>(itemModel);
            _item.IsActive = true;
            await _uow.FormRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        /// <summary>
        /// Check URI exists
        /// </summary>
        /// <param name="URI"></param>
        /// <returns></returns>
        public bool FormExists(string URI)
        {
            return _uow.FormRepo.GetAll().Any(x => x.URI.Equals(URI));
        }

        public List<FormModel> GetAll()
        {
            List<FormModel> items =
               CoreMapper.MapList<Form, FormModel>(_uow.FormRepo.GetAll().ToList());

            return items;
        }

        public List<FormModel> GetAllWithPager(SearchParameters searchParameters)
        {
            try
            {
                var result = _uow.FormRepo.GetAll()
                                            .Include(x => x.Module)
                                            .AsQueryable();

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

                List<FormModel> itemsModels =
                    CoreMapper.MapList<Form, FormModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<FormModel> GetById(int itemId)
        {
            Form result = await _uow.FormRepo.GetById(itemId);

            FormModel itemModel =
                CoreMapper.MapObject<Form, FormModel>(result);

            return itemModel;
        }

        public int Remove(FormModel itemModel)
        {
            Form item = CoreMapper.MapObject<FormModel, Form>(itemModel);
            item.IsActive = false;
            _uow.FormRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(FormModel itemModel)
        {
            Form item = CoreMapper.MapObject<FormModel, Form>(itemModel);
            item.IsActive = true;
            _uow.FormRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

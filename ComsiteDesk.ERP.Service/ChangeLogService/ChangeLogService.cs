using ComsiteDesk.ERP.DB.Core.Models;
using ComsiteDesk.ERP.Service.HelperModel;
using ComsiteDesk.ERP.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public class ChangeLogService : IChangeLogService
    {
        public IUnitOfWork _uow;

        public ChangeLogService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(ChangeLogModel itemModel)
        {
            ChangeLog _item = CoreMapper.MapObject<ChangeLogModel, ChangeLog>(itemModel);
            _item.IsActive = true;
            await _uow.ChangeLogRepo.Insert(_item);
            _uow.Commit();
            return Convert.ToInt32(_item.Id);
        }

        public async Task<List<ChangeLogModel>> GetAllAsync()
        {
            List<ChangeLogModel> items =
                CoreMapper.MapList<ChangeLog, ChangeLogModel>(await _uow.ChangeLogRepo.GetAll().ToListAsync());

            return items;
        }

        public async Task<ChangeLogModel> GetById(int itemId)
        {
            ChangeLog result = await _uow.ChangeLogRepo.GetById(itemId);

            ChangeLogModel itemModel =
                CoreMapper.MapObject<ChangeLog, ChangeLogModel>(result);

            return itemModel;
        }

        public int Remove(ChangeLogModel itemModel)
        {
            ChangeLog item = CoreMapper.MapObject<ChangeLogModel, ChangeLog>(itemModel);
            item.IsActive = false;
            _uow.ChangeLogRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public int Update(ChangeLogModel itemModel)
        {
            ChangeLog item = CoreMapper.MapObject<ChangeLogModel, ChangeLog>(itemModel);
            item.IsActive = true;
            _uow.ChangeLogRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
    }
}

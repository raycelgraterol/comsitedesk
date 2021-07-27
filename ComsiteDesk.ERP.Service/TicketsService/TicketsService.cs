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
    public class TicketsService : ITicketsService
    {
        public IUnitOfWork _uow;
        public TicketsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Add(TicketModel itemModel)
        {
            Tickets _item = CoreMapper.MapObject<TicketModel, Tickets>(itemModel);
            _item.IsActive = true;
            await _uow.TicketsRepo.Insert(_item);
            _uow.Commit();
            itemModel.Id = _item.Id;

            await InserTicketUsersAsync(itemModel);

            return Convert.ToInt32(_item.Id);
        }

        public List<TicketsBalancesModel> GetBalances()
        {
            var result = new List<TicketsBalancesModel>() 
            {
                new TicketsBalancesModel()
                {
                    StatusAlert = statusAlert.primary.ToString(),
                    Title = "Total",
                    Icon = "fe-tag",
                    Total = _uow.TicketsRepo.GetAll()
                            .Count()
                },
                new TicketsBalancesModel()
                {
                    StatusAlert = statusAlert.warning.ToString(),
                    Title = statusTicket.EnProceso.ToString(),
                    Icon = "fe-clock",
                    Total = _uow.TicketsRepo.GetAll()
                            .Where(x => x.TicketStatusId == (int)statusTicket.EnProceso)
                            .Count()
                },
                new TicketsBalancesModel()
                {
                    StatusAlert = statusAlert.success.ToString(),
                    Title = statusTicket.Cerrado.ToString(),
                    Icon = "fe-check-circle",
                    Total = _uow.TicketsRepo.GetAll()
                            .Where(x => x.TicketStatusId == (int)statusTicket.Cerrado)
                            .Count()
                },
                new TicketsBalancesModel()
                {
                    StatusAlert = statusAlert.danger.ToString(),
                    Title = statusTicket.Escalado.ToString(),
                    Icon = "fe-life-buoy",
                    Total = _uow.TicketsRepo.GetAll()
                            .Where(x => x.TicketStatusId == (int)statusTicket.Escalado)
                            .Count()
                }
            };

            return result;
        }

        public async Task<List<TicketModel>> GetAllAsync()
        {
            List<TicketModel> items =
                CoreMapper.MapList<Tickets, TicketModel>(await _uow.TicketsRepo.GetAll().ToListAsync());

            return items;
        }

        public List<TicketModel> GetAllWithPager(TicketsSearchModel searchParameters)
        {
            try
            {
                IQueryable<Tickets> result = _uow.TicketsRepo.GetAllTicketsUsers();

                //count all items
                searchParameters.totalCount = result.Count();

                searchParameters.searchTerm =
                    searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                result = result.Where(s =>
                            ((searchParameters.searchTerm == "") ||
                            s.Title.ToLower().Contains(searchParameters.searchTerm.ToLower()) ||
                            ("t-" + s.Id).ToString().Contains(searchParameters.searchTerm.ToLower())) &&
                            (s.TicketStatusId == searchParameters.TicketStatusId || searchParameters.TicketStatusId == 0) &&
                            (s.StartTime >= searchParameters.StartDate   || searchParameters.StartDate == DateTime.MinValue) &&
                            (s.Users.Any(x => x.UserId == searchParameters.AssignedTo) || searchParameters.AssignedTo == 0));

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

                List<TicketModel> itemsModels =
                    CoreMapper.MapList<Tickets, TicketModel>(result.ToList());

                return itemsModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }       

        public async Task<TicketModel> GetById(int itemId)
        {
            Tickets result = await _uow.TicketsRepo.GetById(itemId);

            TicketModel itemModel =
                CoreMapper.MapObject<Tickets, TicketModel>(result);

            return itemModel;
        }

        public async Task<List<TicketUserModel>> GetAllUsersByTicket(int TicketId)
        {
            var result = _uow.TicketsUsersRepo.GetAll().Where(x => x.TicketsId == TicketId);

            List<TicketUserModel> items =
                CoreMapper.MapList<TicketsUsers, TicketUserModel>(await result.ToListAsync());

            return items;
        }

        public int Remove(TicketModel itemModel)
        {
            Tickets item = CoreMapper.MapObject<TicketModel, Tickets>(itemModel);
            item.IsActive = false;
            _uow.TicketsRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }

        public async Task<int> Update(TicketModel itemModel)
        {
            Tickets item = CoreMapper.MapObject<TicketModel, Tickets>(itemModel);

            await InserTicketUsersAsync(itemModel);

            item.IsActive = true;
            _uow.TicketsRepo.Edit(item);
            _uow.Commit();
            return Convert.ToInt32(item.Id);
        }
        
        private async System.Threading.Tasks.Task InserTicketUsersAsync(TicketModel itemModel)
        {
            try
            {
                var existingItems = _uow.TicketsUsersRepo.GetAll()
                                        .Where(x => x.TicketsId == itemModel.Id)
                                        .ToList();

                //Set is active false for all exist items 
                foreach (var currentItems in existingItems)
                {
                    _uow.TicketsUsersRepo.Delete(currentItems);
                    _uow.Commit();
                }

                //Loop for all formAction ids that come from the user
                foreach (var userId in itemModel.UsersIds)
                {
                    //check if the item exist to update else it is created.
                    var item = _uow.TicketsUsersRepo.GetAll()
                                        .FirstOrDefault(x => x.UserId == userId && x.TicketsId == itemModel.Id);

                    if (item != null)
                    {
                        var ticketUser = new TicketsUsers()
                        {
                            TicketsId = item.TicketsId,
                            UserId = item.UserId,
                            IsActive = true
                        };

                        _uow.TicketsUsersRepo.Edit(ticketUser);
                        _uow.Commit();
                    }
                    else
                    {
                        var ticketUser = new TicketsUsers()
                        {
                            TicketsId = itemModel.Id,
                            UserId = userId,
                            CreatedBy = itemModel.CreatedBy,
                            DateCreated = DateTime.Now,
                            IsActive = true
                        };

                        await _uow.TicketsUsersRepo.Insert(ticketUser);
                        _uow.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

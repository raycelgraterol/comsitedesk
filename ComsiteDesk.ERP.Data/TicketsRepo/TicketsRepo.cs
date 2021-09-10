using ComsiteDesk.ERP.DB.Core;
using ComsiteDesk.ERP.DB.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Data
{
    public class TicketsRepo : Repository<Tickets>, ITicketsRepo
    {
        public TicketsRepo(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Tickets> GetAllTicketsUsers()
        {
            try
            {

                return GetAll()
                            .Include(x => x.TicketStatus)
                            .Include(x => x.TicketCategory)
                            .Include(x => x.TicketType)
                            .Include(x => x.TicketProcess)
                            .Select(
                                x => new Tickets()
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    TicketDate = x.TicketDate,
                                    HoursWorked = x.HoursWorked,
                                    ReportedFailure = x.ReportedFailure,
                                    TechnicalFailure = x.TechnicalFailure,
                                    SolutionDone = x.SolutionDone,
                                    Notes = x.Notes,
                                    StartTime = x.StartTime,
                                    EndTime = x.EndTime,
                                    TicketStatusId = x.TicketStatusId,
                                    TicketStatus = x.TicketStatus,
                                    TicketCategoryId = x.TicketCategoryId,
                                    TicketCategory = x.TicketCategory,
                                    TicketTypeId = x.TicketTypeId,
                                    TicketType = x.TicketType,
                                    TicketProcessId = x.TicketProcessId,
                                    TicketProcess = x.TicketProcess,
                                    ClientId = x.ClientId,
                                    Users = _applicationDbContext.TicketsUsers
                                                            .Include(u => u.User)
                                                            .Where(y => y.IsActive && y.TicketsId == x.Id)
                                                            .Select(tu => new TicketsUsers() {
                                                                TicketsId = tu.TicketsId,
                                                                UserId = tu.UserId,
                                                                CreatedBy = tu.CreatedBy,
                                                                DateModified = tu.DateModified,
                                                                IsActive = tu.IsActive,
                                                                ModifiedBy = tu.ModifiedBy,
                                                                User = tu.User
                                                            })
                                                            .ToList()
                                }
                            ).AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }
    }
}

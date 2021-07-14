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
                            .Join(
                            _applicationDbContext.TicketsUsers
                            .Where(x => x.IsActive).Include(x => x.User),
                                    tickets => tickets.Id,
                                    ticketUser => ticketUser.TicketsId,
                                    (tickets, ticketUser) => new { Ti = tickets, TiUser = ticketUser })
                            .ToList()
                            .GroupBy(
                                p => p.Ti,
                                p => p.TiUser,
                                (key, g) => new { Tickets = key, Users = g.ToList() }
                             )
                            .Select(
                                x => new Tickets()
                                {
                                    Id = x.Tickets.Id,
                                    Title = x.Tickets.Title,
                                    TicketDate = x.Tickets.TicketDate,
                                    HoursWorked = x.Tickets.HoursWorked,
                                    ReportedFailure = x.Tickets.ReportedFailure,
                                    TechnicalFailure = x.Tickets.TechnicalFailure,
                                    SolutionDone = x.Tickets.SolutionDone,
                                    Notes = x.Tickets.Notes,
                                    StartTime = x.Tickets.StartTime,
                                    EndTime = x.Tickets.EndTime,
                                    TicketStatusId = x.Tickets.TicketStatusId,
                                    TicketStatus = x.Tickets.TicketStatus,
                                    TicketCategoryId = x.Tickets.TicketCategoryId,
                                    TicketCategory = x.Tickets.TicketCategory,
                                    TicketTypeId = x.Tickets.TicketTypeId,
                                    TicketType = x.Tickets.TicketType,
                                    TicketProcessId = x.Tickets.TicketProcessId,
                                    TicketProcess = x.Tickets.TicketProcess,
                                    OrganizationId = x.Tickets.OrganizationId,
                                    Users = x.Users
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

using ComsiteDesk.ERP.DB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class TicketModel : ModelBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? TicketDate { get; set; }
        public int HoursWorked { get; set; }
        public string ReportedFailure { get; set; }
        public string TechnicalFailure { get; set; }
        public string SolutionDone { get; set; }
        public string Notes { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        
        public int TicketStatusId { get; set; }
        public int TicketCategoryId { get; set; }
        public int TicketTypeId { get; set; }
        public int? TicketProcessId { get; set; }

        public string TicketStatusName { get; set; }
        public string TicketCategoryName { get; set; }
        public string TicketTypeName { get; set; }
        public string TicketProcessName { get; set; }

        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public long[] UsersIds { get; set; }
        public int[] EquipmentIds { get; set; }
        public TicketUserModel[] Users { get; set; }
    }
}

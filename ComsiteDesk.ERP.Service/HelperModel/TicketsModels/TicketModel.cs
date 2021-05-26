using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class TicketModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TicketDate { get; set; }
        public int HoursWorked { get; set; }
        public string ReportedFailure { get; set; }
        public string TechnicalFailure { get; set; }
        public string SolutionDone { get; set; }
        public string Notes { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        
        public int TicketStatusId { get; set; }
        public TicketStatusModel TicketStatus { get; set; }
        
        public int TicketCategoryId { get; set; }
        public TicketCategoriesModel TicketCategory { get; set; }
        
        public int TicketTypeId { get; set; }
        public TicketTypesModel TicketType { get; set; }
        
        public int? TicketProcessId { get; set; }
        public TicketProcessesModel TicketProcess { get; set; }
        
        public int OrganizationId { get; set; }
        public OrganizationModel Organization { get; set; }

        //public List<TicketsUsers> Users { get; set; }

        //public List<TicketsEquipments> Equipments { get; set; }

    }
}

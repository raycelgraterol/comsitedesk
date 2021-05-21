using ComsiteDesk.ERP.DB.Core.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class Tickets
    {
        public Tickets()
        {
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        public DateTime TicketDate { get; set; }
        public int HoursWorked { get; set; }
        [StringLength(1000)]
        public string ReportedFailure { get; set; }
        [StringLength(1000)]
        public string TechnicalFailure { get; set; }
        [StringLength(1000)]
        public string SolutionDone { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        [Required]
        public int TicketStatusId { get; set; }
        public TicketStatus TicketStatus { get; set; }

        [Required]
        public int TicketCategoryId { get; set; }
        public TicketCategories TicketCategory { get; set; }

        [Required]
        public int TicketTypeId { get; set; }
        public TicketTypes TicketType { get; set; }

        [Required]
        public int? TicketProcessId { get; set; }
        public TicketProcesses TicketProcess { get; set; }

        [Required]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        public List<TicketsUsers> Users { get; set; }

        public List<TicketsEquipments> Equipments { get; set; }
    }
}

using ComsiteDesk.ERP.DB.Core.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class Tickets : ModelBase
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
        [NotMapped]
        public string TicketStatusName {
            get
            {
                return TicketStatus != null ? TicketStatus.Name : "";
            }
        }
        public TicketStatus TicketStatus { get; set; }

        [Required]
        public int TicketCategoryId { get; set; }
        [NotMapped]
        public string TicketCategoryName { 
            get 
            {
                return TicketCategory != null ? TicketCategory.Name : "";
            } 
        }
        public TicketCategories TicketCategory { get; set; }

        [Required]
        public int TicketTypeId { get; set; }
        [NotMapped]
        public string TicketTypeName {
            get
            {
                return TicketType != null ? TicketType.Name : "";
            }
        }
        public TicketTypes TicketType { get; set; }

        [Required]
        public int? TicketProcessId { get; set; }
        [NotMapped]
        public string TicketProcessName {
            get
            {
                return TicketProcess != null ? TicketProcess.Name : "";
            }
        }
        public TicketProcesses TicketProcess { get; set; }

        [Required]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        public List<TicketsUsers> Users { get; set; }

        public List<TicketsEquipments> Equipments { get; set; }
    }
}

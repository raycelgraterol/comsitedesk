using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComsiteDesk.ERP.DB.Core.Models
{
    public class Client: IModelBase
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string BusinessName { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        /// <summary>
        /// Or RIF or ID Card
        /// </summary>
        [Required]
        [StringLength(50)]
        public string IdNumer { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [Required]
        public int ClientTypesId { get; set; }
        public ClientTypes ClientTypes { get; set; }

        [Required]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}

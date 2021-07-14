using ComsiteDesk.ERP.DB.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComsiteDesk.ERP.DB.Core.Authentication
{
    public class User : IdentityUser<long>
    {
        public User()
        {
        }

        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return (FirstName == null ? "" : FirstName) + " " + (LastName == null ? "" : LastName);
            }
        }

        [NotMapped]
        public string keyAccess { get; set; }
        [NotMapped]
        public string Token { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        public List<TicketsUsers> Tickets { get; set; }

    }
}

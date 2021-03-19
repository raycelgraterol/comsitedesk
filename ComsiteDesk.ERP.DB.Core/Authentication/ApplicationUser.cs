using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComsiteDesk.ERP.DB.Core.Authentication
{
    public class User : IdentityUser<long>
    {
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

    }
}

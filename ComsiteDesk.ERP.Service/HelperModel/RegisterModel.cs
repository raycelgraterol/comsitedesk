using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class RegisterModel
    {
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string RolName { get; set; }
        public int OrganizationId { get; set; }
        public string keyAccess { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Email { get; set; }

    }
}

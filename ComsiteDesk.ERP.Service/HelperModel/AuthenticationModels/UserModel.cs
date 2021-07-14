using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class UserModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public IList<string> Roles { get; set; }
        public int OrganizationId { get; set; }
        public OrganizationModel Organization { get; set; }

    }
}

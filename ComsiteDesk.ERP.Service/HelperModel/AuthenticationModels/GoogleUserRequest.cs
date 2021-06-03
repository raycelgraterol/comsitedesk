using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComsiteDesk.ERP.Service.HelperModel
{
    public class GoogleUserRequest
    {
        public const string PROVIDER = "google";

        [JsonProperty("idToken")]
        [Required]
        public string IdToken { get; set; }

        [JsonProperty("userName")]
        [Required]
        public string UserName { get; set; }

        [JsonProperty("firstName")]
        [Required]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("organizationId")]
        public int OrganizationId { get; set; }
    }
}

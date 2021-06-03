using ComsiteDesk.ERP.DB.Core.Authentication;
using ComsiteDesk.ERP.Service.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.Service
{
    public interface IUserService
    {
        Task<User> AuthenticateGoogleUserAsync(GoogleUserRequest request);
    }
}

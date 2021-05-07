using ComsiteDesk.ERP.DB.Core.Authentication;
using ComsiteDesk.ERP.Service.HelperModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace ComsiteDesk.ERP.Service
{
    public class UserService : IUserService
    {
        private readonly RoleManager<Role> roleManager;
        protected readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private IOrganizationsService _organizationsService { get; set; }

        public UserService(
            UserManager<User> userManager, 
            IConfiguration configuration,
            IOrganizationsService organizationsService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _organizationsService = organizationsService;
        }

        public async Task<User> AuthenticateGoogleUserAsync(GoogleUserRequest request)
        {
            Payload payload = await ValidateAsync(request.IdToken, new ValidationSettings
            {
                Audience = new[] { _configuration["Google:ClientId"] }
            });

            return await GetOrCreateExternalLoginUser(GoogleUserRequest.PROVIDER, payload.Subject, payload.Email, payload.GivenName, payload.FamilyName, request.OrganizationId);
        }


        private async Task<User> GetOrCreateExternalLoginUser(string provider, string key, string email, string firstName, string lastName, int organizationId)
        {
            try
            {
                var user = await _userManager.FindByLoginAsync(provider, key);
                if (user != null)
                    return user;
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var organizationCurrentId = organizationId == 0 ? _organizationsService.GetMainOrganization().Id : organizationId;
                    user = new User
                    {
                        Email = email,
                        UserName = email,
                        FirstName = firstName,
                        LastName = lastName,
                        EmailConfirmed = true,
                        OrganizationId = organizationCurrentId
                    };
                    await _userManager.CreateAsync(user);

                    await _userManager.AddToRoleAsync(user, "User");
                }

                var info = new UserLoginInfo(provider, key, provider.ToUpperInvariant());
                var result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                    return user;
                return null;
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }
    }
}

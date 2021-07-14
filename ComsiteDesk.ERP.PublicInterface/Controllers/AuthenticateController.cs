using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ComsiteDesk.ERP.DB.Core.Authentication;
using ComsiteDesk.ERP.DB.Core.Models;
using ComsiteDesk.ERP.Service.HelperModel;
using ComsiteDesk.ERP.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ComsiteDesk.ERP.Service;
using System.Web;
using System.Linq.Dynamic.Core;
using System.IO;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComsiteDesk.ERP.PublicInterface.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        private IOrganizationsService _organizationsService { get; set; }

        public AuthenticateController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration,
            IEmailService emailService,
            IOrganizationsService organizationsService,
            IUserService userService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
            _organizationsService = organizationsService;
            _userService = userService;
        }

        // GET: api/Authenticate/users
        [HttpGet]
        [Route("users")]
        public ActionResult GetUsersPager([FromQuery] SearchParameters searchParameters)
        {
            var items = GetUsersWithPager(searchParameters);

            return Ok(new { data = items, count = searchParameters.totalCount });
        }

        // GET: api/Authenticate/all
        [HttpGet]
        [Route("all")]
        public ActionResult GetAllUsers([FromQuery] SearchParameters searchParameters)
        {
            var items = userManager.Users.ToList();

            return Ok(new { data = items, count = searchParameters.totalCount });
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Username);

            if (user == null)
                user = await userManager.FindByNameAsync(model.Username);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                user.Token = new JwtSecurityTokenHandler().WriteToken(token);

                UserModel userModel = CoreMapper.MapObject<User, UserModel>(user);

                userModel.Organization = await _organizationsService.GetById(user.OrganizationId);

                userModel.Roles = userRoles;

                return Ok(userModel);
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new
            {
                type = ResponseModel.danger,
                Message = ResponseModel.Message = "El Usuario no existe. Registrese para continuar."
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("googleLogin")]
        public async Task<IActionResult> GoogleAuthenticate([FromBody] GoogleUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.SelectMany(it => it.Errors).Select(it => it.ErrorMessage));

                var userModel = await GenerateUserToken(await _userService.AuthenticateGoogleUserAsync(request));

                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { 
                        type = ResponseModel.danger, 
                        Message = ResponseModel.Message = "Ha Ocurrido un Error inesperado." });
            }
            
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var userExists = await userManager.FindByNameAsync(model.UserName);

                var currentOrganizationId = model.OrganizationId == 0 ? _organizationsService.GetMainOrganization().Id : model.OrganizationId;

                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { type = ResponseModel.danger, Message = ResponseModel.Message = "¡El usuario ya existe!" });

                User user = new User()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    OrganizationId = currentOrganizationId
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    if (result.Errors.Count() > 0)
                    {
                        foreach (var error in result.Errors)
                        {
                            switch (error.Code)
                            {
                                case "PasswordTooShort":
                                    ResponseModel.Message += "La contraseña es muy corta para ser aceptada.";
                                    break;
                                case "PasswordRequiresUpper":
                                    ResponseModel.Message += "La contraseña debe contener una Mayuscula. ";
                                    break;
                                case "PasswordRequiresNonAlphanumeric":
                                    ResponseModel.Message += "La contraseña debe contener un Simbolo. ";
                                    break;
                                case "PasswordRequiresLower":
                                    ResponseModel.Message += "La contraseña debe contener al menos una letra minuscula. ";
                                    break;
                                default:
                                    ResponseModel.Message += "Error al crear el usuario. Compruebe los datos del usuario y vuelva a intentarlo.";
                                    break;
                            }
                        }
                    }

                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new
                        {
                            type = ResponseModel.danger,
                            message = ResponseModel.Message
                        });
                }

                if (await roleManager.RoleExistsAsync(model.RolName))
                {
                    await userManager.AddToRoleAsync(user, model.RolName);
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "User");
                }

                var mailTo = user.Email;
                var subject = string.Format("Usuario {0} Creado Exitosamente", model.RolName);
                var html = string.Format("El usuario: {0} fue creado exitosamente. <br/> Con la contraseña: {1} <br/>", model.UserName , model.Password);

                _emailService.Send(mailTo, subject, html);

                return Ok(new { type = ResponseModel.success, message = ResponseModel.Message = "¡Usuario creado con éxito!" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Register Admin
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-admin")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            try
            {
                if (model.keyAccess != _configuration["JWT:keyAccess"])
                    return StatusCode(StatusCodes.Status500InternalServerError, new { type = ResponseModel.danger, message = ResponseModel.Message = "¡Llave de acceso incorrecta!" });

                var currentOrganizationId = model.OrganizationId == 0 ? _organizationsService.GetMainOrganization().Id : model.OrganizationId;

                var userExists = await userManager.FindByNameAsync(model.Email);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { type = ResponseModel.danger, message = ResponseModel.Message = "¡El usuario ya existe!" });

                User user = new User()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    OrganizationId = currentOrganizationId
                };

                ResponseModel.Message = "";

                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    if (result.Errors.Count() > 0)
                    {
                        foreach (var error in result.Errors)
                        {
                            switch (error.Code)
                            {
                                case "PasswordTooShort":
                                    ResponseModel.Message += "La contraseña es muy corta para ser aceptada.";
                                    break;
                                case "PasswordRequiresUpper":
                                    ResponseModel.Message += "La contraseña debe contener una Mayuscula. ";
                                    break;
                                case "PasswordRequiresNonAlphanumeric":
                                    ResponseModel.Message += "La contraseña debe contener un Simbolo. ";
                                    break;
                                case "PasswordRequiresLower":
                                    ResponseModel.Message += "La contraseña debe contener al menos una letra minuscula. ";
                                    break;
                                default:
                                    ResponseModel.Message += "Error al crear el usuario. Compruebe los datos del usuario y vuelva a intentarlo.";
                                    break;
                            }
                        }
                    }

                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new
                        {
                            type = ResponseModel.danger,
                            message = ResponseModel.Message
                        });
                }


                //Create role if do not exist.
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new Role() { Name = UserRoles.Admin });
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new Role() { Name = UserRoles.User });

                if (await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                }

                var mailTo = user.Email;
                var subject = "Usuario Administrador Creado Exitosamente";
                var html = string.Format("El usuario: {0} fue creado exitosamente. <br/> Con la contraseña: {1} <br/>", user.UserName, model.Password);

                _emailService.Send(mailTo, subject, html);

                ResponseModel.Message = "¡Usuario creado con éxito!";

                return Ok(new { type = ResponseModel.success, Message = ResponseModel.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new
                        {
                            type = ResponseModel.danger,
                            message = ResponseModel.Message
                        });
            }
            
        }

        // GET api/Authenticate/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var item = userManager.Users.FirstOrDefault(x => x.Id == id);

            UserModel userModel = CoreMapper.MapObject<User, UserModel>(item);

            var userRoles = await userManager.GetRolesAsync(item);

            userModel.Roles = userRoles.Take(1).ToList();
            
            if (item == null)
            {
                return NotFound();
            }

            return Ok(new { data = userModel, id });
        }                

        /// <summary>
        /// Update User
        /// </summary>
        /// <returns></returns>
        /// PUT: api/Authenticate/update
        [HttpPost, DisableRequestSizeLimit]
        [Route("update")]
        public async Task<IActionResult> UpdateUser()
        {
            try
            {
                var dbPath = "";                

                var user = new User()
                {
                    UserName = Request.Form["UserName"],
                    FirstName = Request.Form["FirstName"],
                    LastName = Request.Form["LastName"],
                    PhoneNumber = Request.Form["PhoneNumber"]
                }; 
                
                var currentUser = await userManager.FindByEmailAsync(user.UserName);

                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.PhoneNumber = user.PhoneNumber;

                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    var folderName = Path.Combine("StaticFiles", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToLowerInvariant().Trim('"').Replace(" ", "-");
                        var fullPath = Path.Combine(pathToSave, fileName);
                        dbPath = Path.Combine(folderName, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        currentUser.ImageUrl = dbPath;
                    }
                }               

                IdentityResult result = await userManager.UpdateAsync(currentUser);

                UserModel userModel = CoreMapper.MapObject<User, UserModel>(currentUser);

                userModel.Organization = await _organizationsService.GetById(userModel.OrganizationId);

                userModel.Roles = await userManager.GetRolesAsync(currentUser);

                ResponseModel.Status = "Success";
                ResponseModel.Message = "¡Usuario creado con éxito!";

                return Ok(new { type = ResponseModel.success, message = ResponseModel.Message, user = currentUser });
            }
            catch (Exception ex)
            {
                ResponseModel.Status = "Error";
                ResponseModel.Message = "Ha Ocurrido un error al actualizar el Usuario.";

                return Ok(new { type = ResponseModel.danger, message = ResponseModel.Message });
            }
        }

        /// <summary>
        /// Password reset
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        /// POST: api/Authenticate/passwordreset
        [HttpPost]
        [Route("passwordreset")]
        public async Task<IActionResult> PasswordReset([FromBody] LoginModel loginModel)
        {
            try
            {
                var newPass = loginModel.Password;

                var changeUser = await userManager.FindByNameAsync(loginModel.Username);

                await userManager.RemovePasswordAsync(changeUser);

                var result = await userManager.AddPasswordAsync(changeUser, newPass);

                if (!result.Succeeded)
                {
                    if (result.Errors.Count() > 0)
                    {
                        foreach (var error in result.Errors)
                        {
                            switch (error.Code)
                            {
                                case "PasswordRequiresUpper":
                                    ResponseModel.Message += "La contraseña debe contener una Mayuscula. ";
                                    break;
                                case "PasswordRequiresNonAlphanumeric":
                                    ResponseModel.Message += "La contraseña debe contener un Simbolo. ";
                                    break;
                                case "PasswordRequiresLower":
                                    ResponseModel.Message += "La contraseña debe contener al menos una letra minuscula. ";
                                    break;
                                default:
                                    ResponseModel.Message += "No se pudo restaurar la contraseña.";
                                    break;
                            }
                        }
                    }

                    return Ok(new { message = ResponseModel.Message, type = ResponseModel.danger });
                }

                ResponseModel.Message = "La Contraseña fue cambiada con exito.";

                return Ok(new { message = ResponseModel.Message, type = ResponseModel.success });

            }
            catch (Exception ex)
            {
                ResponseModel.Message = "Ha ocurrido un error inesperado.";
                return Ok(new { message = ResponseModel.Message, type = ResponseModel.danger });
            }
        }
        /// <summary>
        /// Forgot password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// POST: api/Authenticate/forgot-password
        [HttpPost]
        [Route("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { type = ResponseModel.danger, message = ResponseModel.Message = "¡El usuario no existe!" });

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            token = HttpUtility.UrlEncode(token);

            var callback = @"http://localhost:4200/account/passwordchange?email=" + user.Email + "&token=" + token;

            var mailTo = model.Email;
            var subject = "Recuperacion de contraseña";
            var html = "Correo para la recuperacion de la contraseña. <br/> " + callback;

            _emailService.Send(mailTo, subject, html);

            return Ok(
                new { 
                    type = ResponseModel.success, 
                    message = ResponseModel.Message = "Le hemos enviado un correo electrónico con un enlace para restablecer su contraseña." 
                });
        }
        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// POST: api/Authenticate/reset-password
        [HttpPost]
        [Route("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { type = ResponseModel.danger, message = ResponseModel.Message = "¡El usuario no existe!" });

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (!result.Succeeded)
            {
                if (result.Errors.Count() > 0)
                {
                    ResponseModel.Message = "";
                    foreach (var error in result.Errors)
                    {
                        switch (error.Code)
                        {
                            case "PasswordRequiresUpper":
                                ResponseModel.Message += " La contraseña debe contener una Mayuscula. ";
                                break;
                            case "PasswordRequiresNonAlphanumeric":
                                ResponseModel.Message += " La contraseña debe contener un Simbolo. ";
                                break;
                            case "PasswordRequiresLower":
                                ResponseModel.Message += " La contraseña debe contener al menos una letra minuscula. ";
                                break;
                            default:
                                ResponseModel.Message += " No se pudo restaurar la contraseña.";
                                break;
                        }
                    }
                }

                return Ok(new { message = ResponseModel.Message, type = ResponseModel.danger });
            }

            return Ok(new { type = ResponseModel.success, message = ResponseModel.Message = "¡La Contraseña se ha cambiado exitosamente!" });
        }

        /// <summary>
        /// Get Users with pager
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private List<User> GetUsersWithPager(SearchParameters searchParameters)
        {
            try
            {
                searchParameters.searchTerm = searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                var resultTotal = userManager.Users
                            .Where(o => o.OrganizationId == searchParameters.organizationId)
                            .Where(s =>
                            (searchParameters.searchTerm == "") ||
                            s.FirstName.ToLower().Contains(searchParameters.searchTerm.ToLower()) ||
                            s.UserName.ToLower().Contains(searchParameters.searchTerm.ToLower()) ||
                            s.Id.ToString().Contains(searchParameters.searchTerm.ToLower()));

                //Count after filter total result
                searchParameters.totalCount = resultTotal.Count();

                if (searchParameters.sortColumn != null)
                {
                    //Sorting
                    resultTotal = resultTotal
                            .OrderBy(searchParameters.sortColumn + " " + searchParameters.sortDirection);
                }
                else
                {
                    resultTotal = resultTotal.OrderByDescending(x => x.Id);
                }

                resultTotal = resultTotal
                            .Skip(searchParameters.startIndex)
                            .Take(searchParameters.PageSize);


                return resultTotal.ToList();

            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }

        #region Private Methods
        private async Task<UserModel> GenerateUserToken(User user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

                var expires = DateTime.UtcNow.AddDays(7);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Surname, user.FirstName),
                    new Claim(ClaimTypes.GivenName, user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                }),

                    Expires = expires,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"]
                };

                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                
                user.Organization = null;
                user.Token = tokenHandler.WriteToken(securityToken);

                UserModel userModel = CoreMapper.MapObject<User, UserModel>(user);

                userModel.Organization = await _organizationsService.GetById(user.OrganizationId);

                var userRoles = await userManager.GetRolesAsync(user);

                userModel.Roles = userRoles;

                return userModel;

                /*
                return new UserToken
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Token = token,
                    Expires = expires
                };
                */
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        #endregion
    }
}

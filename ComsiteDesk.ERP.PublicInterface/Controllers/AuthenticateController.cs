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

        public AuthenticateController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        // GET: api/Authenticate/users
        [HttpGet]
        [Route("users")]
        public ActionResult GetAllUsers()
        {
            var items = userManager.Users;

            return Ok(new { data = items, count = items.Count() });
        }

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

                userModel.Roles = userRoles;                

                return Ok(userModel);
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new
            {
                type = ResponseModel.danger,
                Message = ResponseModel.Message = "El Usuario no existe. Registrese para continuar."
            });
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);

            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { type = ResponseModel.danger, Message = ResponseModel.Message = "¡El usuario ya existe!" });

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    type = ResponseModel.danger,
                    Message = ResponseModel.Message = "Error al crear el usuario. Compruebe los datos del usuario y vuelva a intentarlo."
                });

            return Ok(new { type = ResponseModel.success, message = ResponseModel.Message = "¡Usuario creado con éxito!" });
        }

        [HttpPost]
        [Route("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] LoginModel loginModel)
        {
            var userExists = await userManager.FindByEmailAsync(loginModel.Username);
            if (userExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { type = ResponseModel.danger, message = ResponseModel.Message = "¡El usuario no existe!" });

            var result = await userManager.ResetPasswordAsync(userExists, userExists.Token, "1234");

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

            return Ok(new { type = ResponseModel.success, message = ResponseModel.Message = "¡Hemos enviado la info a su Email!" });
        }

        [HttpPost]
        [Route("register-admin")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            if (model.keyAccess != _configuration["JWT:keyAccess"])
                return StatusCode(StatusCodes.Status500InternalServerError, new { type = ResponseModel.danger, message = ResponseModel.Message = "¡Llave de acceso incorrecta!" });

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
                OrganizationId = model.OrganizationId
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
            ResponseModel.Message = "¡Usuario creado con éxito!";

            return Ok(new { type = ResponseModel.success, Message = ResponseModel.Message });
        }

        // POST: api/users/update
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser()
        {
            try
            {
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

                IdentityResult result = await userManager.UpdateAsync(currentUser);

                ResponseModel.Status = "Success";
                ResponseModel.Message = "¡Usuario creado con éxito!";

                return Ok(new { type = ResponseModel.Status, message = ResponseModel.Message, user = currentUser });
            }
            catch (Exception ex)
            {
                ResponseModel.Status = "Error";
                ResponseModel.Message = "Ha Ocurrido un error al actualizar el Usuario.";

                return Ok(new { type = ResponseModel.Status, message = ResponseModel.Message });
            }
        }

        /// <summary>
        /// Reset password from the dashboard
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
    }
}

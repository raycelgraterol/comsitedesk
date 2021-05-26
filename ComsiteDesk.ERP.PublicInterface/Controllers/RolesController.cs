using ComsiteDesk.ERP.DB.Core.Authentication;
using ComsiteDesk.ERP.Service.HelperModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComsiteDesk.ERP.PublicInterface.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> roleManager;

        public RolesController(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        // GET: api/<RolesController>
        [HttpGet]
        public ActionResult Get()
        {
            var items = roleManager.Roles.ToList();

            return Ok(new { data = items, count = items.Count() });
        }


        [HttpGet]
        [Route("all")]
        public ActionResult GetAll([FromQuery] SearchParameters searchParameters)
        {
            var items = GetRolesWithPager(searchParameters);

            return Ok(new { data = items, count = searchParameters.totalCount });
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            var item = roleManager.Roles.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(new { data = item, id });
        }

        // POST api/<RolesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Role rol)
        {
            //Create role if do not exist.
            if (await roleManager.RoleExistsAsync(rol.Name))
            {
                ResponseModel.Message = "¡Este Rol Ya Existe!";

                return Ok(new { type = ResponseModel.success, Message = ResponseModel.Message });
            }
            

            await roleManager.CreateAsync(new Role() { Name = rol.Name });

            ResponseModel.Message = "¡Rol creado con éxito!";

            return Ok(new { type = ResponseModel.success, Message = ResponseModel.Message });

        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] Role rol)
        {
            try
            {
                var currentItem = await roleManager.FindByIdAsync(rol.Id.ToString());

                currentItem.Name = rol.Name;

                var result = await roleManager.UpdateAsync(currentItem);

                if (!result.Succeeded)
                {
                    if (result.Errors.Count() > 0)
                    {
                        ResponseModel.Message = result.Errors.FirstOrDefault().Description;
                    }

                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new
                        {
                            type = ResponseModel.danger,
                            message = ResponseModel.Message
                        });
                }

                ResponseModel.Status = "Success";
                ResponseModel.Message = "¡Rol creado con éxito!";

                return Ok(new { type = ResponseModel.Status, message = ResponseModel.Message, rol = currentItem });
            }
            catch (Exception ex)
            {
                ResponseModel.Status = "Error";
                ResponseModel.Message = "Ha Ocurrido un error al actualizar el Rol.";

                return Ok(new { type = ResponseModel.Status, message = ResponseModel.Message });
            }
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /// <summary>
        /// Get Roles with pager
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private List<Role> GetRolesWithPager(SearchParameters searchParameters)
        {
            try
            {
                searchParameters.searchTerm = searchParameters.searchTerm == null ? "" : searchParameters.searchTerm;

                //Filters
                var resultTotal = roleManager.Roles
                            .Where(s =>
                            (searchParameters.searchTerm == "") ||
                            s.Name.ToLower().Contains(searchParameters.searchTerm.ToLower()) ||
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
                return new List<Role>();
            }

        }
    }
}

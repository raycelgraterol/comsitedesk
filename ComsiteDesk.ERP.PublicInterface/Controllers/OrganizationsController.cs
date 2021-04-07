using ComsiteDesk.ERP.Service;
using ComsiteDesk.ERP.Service.HelperModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComsiteDesk.ERP.PublicInterface.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IOrganizationsService _organizationsService { get; set; }
        public OrganizationsController(IOrganizationsService organizationsService,
            IConfiguration configuration)
        {
            _organizationsService = organizationsService;
            _configuration = configuration;
        }

        // GET: api/<OrganizationsController>
        [HttpGet]
        public ActionResult Get()
        {
            var items = _organizationsService.GetAll();

            return Ok(new { data = items, count = items.Count});
        }

        // GET api/<OrganizationsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var item = await _organizationsService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(new { data = item, id });
        }

        // POST api/<OrganizationsController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] OrganizationModel value)
        {
            if (value.keyAccess != _configuration["JWT:keyAccess"])
                return StatusCode(StatusCodes.Status500InternalServerError, new { type = ResponseModel.danger, message = ResponseModel.Message = "¡Llave de acceso incorrecta!" });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            value.CreatedBy = userId;
            value.DateCreated = DateTime.Now;

            var id = await _organizationsService.Add(value);
            return Ok(new { data = value, id });
        }

        // PUT api/<OrganizationsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] OrganizationModel value)
        {
            var currentValue = await _organizationsService.GetById(value.Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            value.DateCreated = currentValue.DateCreated;
            value.CreatedBy = currentValue.CreatedBy;

            value.ModifiedBy = userId;
            value.DateModified = DateTime.Now;

            var item = _organizationsService.Update(value);

            return Ok(new { data = value });
        }

        // DELETE api/<OrganizationsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var existingItem = await _organizationsService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            existingItem.ModifiedBy = userId;
            existingItem.DateModified = DateTime.Now;

            _organizationsService.Remove(existingItem);

            return Ok(new { data = existingItem, id });
        }
    }
}

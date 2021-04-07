using ComsiteDesk.ERP.Service;
using ComsiteDesk.ERP.Service.HelperModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComsiteDesk.ERP.PublicInterface.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private IClientsService _clientService { get; set; }
        public ClientsController(IClientsService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/<ClientsController>
        [HttpGet]
        public ActionResult Get([FromQuery] SearchParameters searchParameters)
        {
            var items = _clientService.GetAllWithPager(searchParameters);

            return Ok(new { data = items, count = searchParameters.CountItems });
        }

        // GET api/<ClientsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var item = _clientService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(new { data = item, id });
        }

        // POST api/<ClientsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClientModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            value.CreatedBy = userId;
            value.DateCreated = DateTime.Now;

            var id = await _clientService.Add(value);
            return Ok(new { data = value });
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] ClientModel value)
        {
            var currentValue = await _clientService.GetById(value.Id);

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

            var item = _clientService.Update(value);

            return Ok(new { data = value });
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var existingItem = await _clientService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            existingItem.ModifiedBy = userId;
            existingItem.DateModified = DateTime.Now;

            _clientService.Remove(existingItem);

            return Ok(new { data = existingItem, id });
        }
    }
}

using ComsiteDesk.ERP.Service;
using ComsiteDesk.ERP.Service.HelperModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComsiteDesk.ERP.PublicInterface.Controllers
{
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
        public ActionResult Post([FromBody] ClientModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _clientService.Add(value);
            return Ok(new { data = value });
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] ClientModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _clientService.Update(value);

            return Ok(new { data = value });
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            var existingItem = _clientService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _clientService.Remove(existingItem);

            return Ok(new { data = existingItem, id });
        }
    }
}

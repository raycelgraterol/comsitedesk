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
    public class OrganizationsController : ControllerBase
    {
        private IOrganizationsService _organizationsService { get; set; }
        public OrganizationsController(IOrganizationsService organizationsService)
        {
            _organizationsService = organizationsService;
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
        public ActionResult Get(int id)
        {
            var item = _organizationsService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(new { data = item, id });
        }

        // POST api/<OrganizationsController>
        [HttpPost]
        public ActionResult Post([FromBody] OrganizationModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _organizationsService.Add(value);
            return Ok(new { data = value });
        }

        // PUT api/<OrganizationsController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] OrganizationModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _organizationsService.Update(value);

            return Ok(new { data = value });
        }

        // DELETE api/<OrganizationsController>/5
        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            var existingItem = _organizationsService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _organizationsService.Remove(existingItem);

            return Ok(new { data = existingItem, id });
        }
    }
}

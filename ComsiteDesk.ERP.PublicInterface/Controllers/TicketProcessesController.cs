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
    public class TicketProcessesController : ControllerBase
    {
        public ITicketProcessesService _ticketProcessesService { get; set; }

        public TicketProcessesController(ITicketProcessesService ticketProcessesService)
        {
            _ticketProcessesService = ticketProcessesService;
        }

        // GET: api/TicketProcesses
        [HttpGet]
        public ActionResult GetAllWithPager([FromQuery] SearchParameters searchParameters)
        {
            var items = _ticketProcessesService.GetAllWithPager(searchParameters);

            return Ok(new { data = items, count = searchParameters.totalCount });
        }

        // GET: api/TicketProcesses/All
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult> Get()
        {
            var items = await _ticketProcessesService.GetAllAsync();

            return Ok(new { data = items, count = items.Count });
        }

        // GET: api/TicketProcesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var item = await _ticketProcessesService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(new { data = item });
        }

        // POST: api/TicketProcesses
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TicketProcessesModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            value.CreatedBy = userId;
            value.DateCreated = DateTime.Now;

            var id = await _ticketProcessesService.Add(value);

            return Ok(new { data = id });

        }

        // PUT: api/TicketProcesses/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] TicketProcessesModel value)
        {
            var currentValue = await _ticketProcessesService.GetById(value.Id);

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

            var item = _ticketProcessesService.Update(value);

            return Ok(new { data = item });
        }

        // DELETE: api/TicketProcesses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var existingItem = await _ticketProcessesService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            existingItem.ModifiedBy = userId;
            existingItem.DateModified = DateTime.Now;

            _ticketProcessesService.Remove(existingItem);

            return Ok(new { data = id });
        }
    }
}

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
    public class TicketsController : ControllerBase
    {
        public ITicketsService _ticketsService { get; set; }

        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        // GET: api/Tickets/Balances
        [HttpGet]
        [Route("Balances")]
        public ActionResult GetBalances()
        {
            var items =  _ticketsService.GetBalances();

            return Ok(items);
        }

        // GET: api/Tickets
        [HttpGet]
        public ActionResult GetAllWithPager([FromQuery] TicketsSearchModel searchParameters)
        {
            var items = _ticketsService.GetAllWithPager(searchParameters);

            return Ok(new { data = items, count = searchParameters.totalCount });
        }

        // GET: api/Tickets/All
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult> Get()
        {
            var items = await _ticketsService.GetAllAsync();

            return Ok(new { data = items, count = items.Count });
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var item = await _ticketsService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(new { data = item });
        }

        // POST api/Tickets/GetListUsersByTicket
        [HttpPost]
        [Route("GetListUsersByTicket")]
        public async Task<ActionResult> GetListUsersByTicket([FromBody] TicketModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _ticketsService.GetAllUsersByTicket(value.Id);

            return Ok(new { data = result, count = result.Count() });
        }

        // POST: api/Tickets
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TicketModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            value.CreatedBy = userId;
            value.DateCreated = DateTime.Now;

            var id = await _ticketsService.Add(value);

            return Ok(new { data = id });

        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] TicketModel value)
        {
            var currentValue = await _ticketsService.GetById(value.Id);

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

            var item = await _ticketsService.Update(value);

            return Ok(new { data = item });
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var existingItem = await _ticketsService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            existingItem.ModifiedBy = userId;
            existingItem.DateModified = DateTime.Now;

            _ticketsService.Remove(existingItem);

            return Ok(new { data = id });
        }
    }
}

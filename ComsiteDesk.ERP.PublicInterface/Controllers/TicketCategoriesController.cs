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
    public class TicketCategoriesController : ControllerBase
    {
        public ITicketCategoriesService _ticketCategoriesService { get; set; }

        public TicketCategoriesController(ITicketCategoriesService ticketCategoriesService)
        {
            _ticketCategoriesService = ticketCategoriesService;
        }

        // GET: api/TicketCategories
        [HttpGet]
        public ActionResult GetAllWithPager([FromQuery] SearchParameters searchParameters)
        {
            var items = _ticketCategoriesService.GetAllWithPager(searchParameters);

            return Ok(new { data = items, count = searchParameters.totalCount });
        }

        // GET: api/TicketCategories/All
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult> Get()
        {
            var items = await _ticketCategoriesService.GetAllAsync();

            return Ok(new { data = items, count = items.Count });
        }

        // GET: api/TicketCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var item = await _ticketCategoriesService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(new { data = item });
        }

        // POST: api/TicketCategories
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TicketCategoriesModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            value.CreatedBy = userId;
            value.DateCreated = DateTime.Now;

            var id = await _ticketCategoriesService.Add(value);

            return Ok(new { data = id });

        }

        // PUT: api/TicketCategories/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] TicketCategoriesModel value)
        {
            var currentValue = await _ticketCategoriesService.GetById(value.Id);

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

            var item = _ticketCategoriesService.Update(value);

            return Ok(new { data = item });
        }

        // DELETE: api/TicketCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var existingItem = await _ticketCategoriesService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            existingItem.ModifiedBy = userId;
            existingItem.DateModified = DateTime.Now;

            _ticketCategoriesService.Remove(existingItem);

            return Ok(new { data = id });
        }
    }
}

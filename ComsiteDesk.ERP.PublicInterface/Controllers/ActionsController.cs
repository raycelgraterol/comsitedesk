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
    public class ActionsController : ControllerBase
    {
        public IActionService _actionService { get; set; }

        public ActionsController(IActionService actionService)
        {
            _actionService = actionService;
        }

        // GET: api/Actions
        [HttpGet]
        public ActionResult GetBanks([FromQuery] SearchParameters searchParameters)
        {
            var items = _actionService.GetAllWithPager(searchParameters);

            return Ok(new { data = items, count = searchParameters.totalCount });
        }

        // GET: api/Actions/All
        [HttpGet]
        [Route("All")]
        public ActionResult Get()
        {
            var items = _actionService.GetAll();

            return Ok(new { data = items, count = items.Count });
        }

        // GET: api/Actions/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var item = await _actionService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(new { data = item });
        }

        // POST: api/Actions
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ActionModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            value.CreatedBy = userId;
            value.DateCreated = DateTime.Now;

            var id = await _actionService.Add(value);

            return Ok(new { data = id });

        }

        // PUT: api/Actions/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] ActionModel value)
        {
            var currentValue = await _actionService.GetById(value.Id);

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

            var item = _actionService.Update(value);

            return Ok(new { data = item });
        }

        // DELETE: api/Actions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var existingItem = await _actionService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            existingItem.ModifiedBy = userId;
            existingItem.DateModified = DateTime.Now;

            _actionService.Remove(existingItem);

            return Ok(new { data = id });
        }
    }
}

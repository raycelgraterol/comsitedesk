using ComsiteDesk.ERP.Service;
using ComsiteDesk.ERP.Service.HelperModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.PublicInterface.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentUserController : Controller
    {
        public IEquipmentUserService _equipmentUserService { get; set; }

        public EquipmentUserController(IEquipmentUserService equipmentUserService)
        {
            _equipmentUserService = equipmentUserService;
        }

        // GET: api/EquipmentUser
        [HttpGet]
        public ActionResult GetAllWithPager([FromQuery] SearchParameters searchParameters)
        {
            var items = _equipmentUserService.GetAllWithPager(searchParameters);

            return Ok(new { data = items, count = searchParameters.totalCount });
        }

        // GET: api/EquipmentUser/All
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult> Get()
        {
            var items = await _equipmentUserService.GetAllAsync();

            return Ok(new { data = items, count = items.Count });
        }

        // GET: api/EquipmentUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var item = await _equipmentUserService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(new { data = item });
        }

        // POST: api/EquipmentUser
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EquipmentUserModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            value.CreatedBy = userId;
            value.DateCreated = DateTime.Now;

            var id = await _equipmentUserService.Add(value);

            return Ok(new { data = id });

        }

        // PUT: api/EquipmentUser/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] EquipmentUserModel value)
        {
            var currentValue = await _equipmentUserService.GetById(value.Id);

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

            var item = _equipmentUserService.Update(value);

            return Ok(new { data = item });
        }

        // DELETE: api/EquipmentUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var existingItem = await _equipmentUserService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            existingItem.ModifiedBy = userId;
            existingItem.DateModified = DateTime.Now;

            _equipmentUserService.Remove(existingItem);

            return Ok(new { data = id });
        }
    }
}

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
    public class FormsController : ControllerBase
    {
        public IFormService _formService { get; set; }

        public FormsController(IFormService formService)
        {
            _formService = formService;
        }

        // GET: api/Forms
        [HttpGet]
        public ActionResult GetBanks([FromQuery] SearchParameters searchParameters)
        {
            var items = _formService.GetAllWithPager(searchParameters);

            return Ok(new { data = items, count = searchParameters.totalCount });
        }

        // GET: api/Forms/All
        [HttpGet]
        [Route("All")]
        public ActionResult Get()
        {
            var items = _formService.GetAll();

            return Ok(new { data = items, count = items.Count });
        }

        // POST api/Forms/ViewsByModule
        [HttpPost]
        [Route("ViewsByModule")]
        public ActionResult GetViewsByModule([FromBody] FormModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _formService.GetAll().Where(x => x.ModuleId == value.ModuleId);

            return Ok(new { data = result, count = result.Count() });
        }

        // GET: api/Forms/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var item = await _formService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(new { data = item });
        }

        // POST: api/Forms
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FormModel value)
        {
            //Create role if do not exist.
            if (_formService.FormExists(value.URI))
            {
                ResponseModel.Message = "¡Esta Vista Ya Existe!";

                return Ok(new { type = ResponseModel.danger, Message = ResponseModel.Message });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            value.CreatedBy = userId;
            value.DateCreated = DateTime.Now;

            var id = await _formService.Add(value);

            ResponseModel.Message = "¡Vista Creada con Exito!";

            return Ok(new { data = id, type = ResponseModel.success, Message = ResponseModel.Message });
        }

        // PUT: api/Forms/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] FormModel value)
        {
            var currentValue = await _formService.GetById(value.Id);

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

            var item = _formService.Update(value);

            return Ok(new { data = item });
        }

        // DELETE: api/Forms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var existingItem = await _formService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            existingItem.ModifiedBy = userId;
            existingItem.DateModified = DateTime.Now;

            _formService.Remove(existingItem);

            return Ok(new { data = id });
        }
    }
}

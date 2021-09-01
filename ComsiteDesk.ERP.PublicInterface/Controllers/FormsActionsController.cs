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
    public class FormsActionsController : ControllerBase
    {
        public IFormActionService _formActionService { get; set; }
        public FormsActionsController(IFormActionService formActionService)
        {
            _formActionService = formActionService;
        }

        // GET: api/<FormsActionsController>
        [HttpGet]
        public ActionResult Get([FromQuery] SearchParameters searchParameters)
        {
            var items = _formActionService.GetAllWithPager(searchParameters);

            return Ok(new { data = items, count = searchParameters.totalCount });
        }

        // GET: api/FormsActions/All
        [HttpGet]
        [Route("All")]
        public ActionResult Get()
        {
            var items = _formActionService.GetAll();

            return Ok(new { data = items, count = items.Count });
        }        

        // POST api/FormsActions/ActionsByForm
        [HttpPost]
        [Route("ActionsByForm")]
        public ActionResult GetActionsByForm([FromBody] RolFormActionSearch value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _formActionService.GetAll().Where(x => x.FormId == value.FormId);

            return Ok(new { data = result, count = result.Count() });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var item = await _formActionService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(new { data = item });
        }

        // POST api/<FormsActionsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FormActionModel value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                int userId;
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

                value.CreatedBy = userId;
                value.DateCreated = DateTime.Now;

                var id = await _formActionService.Add(value);

                ResponseModel.Message = "¡Accion creado con éxito!";

                return Ok(new { data = id, type = ResponseModel.success, Message = ResponseModel.Message });
            }
            catch (Exception ex)
            {
                ResponseModel.Message = "¡Ha ocurrido un error, revise e intente de nuevo!";

                return Ok(new { type = ResponseModel.danger, Message = ResponseModel.Message });
            }
            
        }

        // POST api/FormsActions
        [HttpPost]
        [Route("GetListFormActions")]
        public ActionResult GetListFormActions([FromBody] RolFormActionSearch value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _formActionService.GetAllByRolAndForm(value.RoleId, value.FormId);

            return Ok(new { data = result, count = result.Count() });
        }

        // PUT api/<FormsActionsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] FormActionModel value)
        {
            var currentValue = await _formActionService.GetById(value.Id);

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

            var item = _formActionService.Update(value);

            return Ok(new { data = item });
        }

        // DELETE api/<FormsActionsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var existingItem = await _formActionService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);

            existingItem.ModifiedBy = userId;
            existingItem.DateModified = DateTime.Now;

            _formActionService.Remove(existingItem);

            return Ok(new { data = id });
        }
    }
}

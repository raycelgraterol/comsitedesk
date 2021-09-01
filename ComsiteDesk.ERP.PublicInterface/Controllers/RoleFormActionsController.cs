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
    public class RoleFormActionsController : ControllerBase
    {
        public IRoleFormActionService _roleFormActionService { get; set; }

        public RoleFormActionsController(IRoleFormActionService roleFormActionService)
        {
            _roleFormActionService = roleFormActionService;
        }

        [HttpGet]
        public ActionResult GetAll([FromQuery] SearchParameters searchParameters)
        {
            var items = _roleFormActionService.GetAllWithPager(searchParameters);

            return Ok(new { data = items, count = searchParameters.totalCount });
        }

        // POST: api/RoleFormActions/CheckRoleModule
        [HttpPost]
        [Route("CheckRoleModule")]
        public ActionResult CheckRoleModule([FromBody] RoleFormActionModel value)
        {
            var CanViewModule = _roleFormActionService.CheckRoleModule(value);

            return Ok( CanViewModule );
        }

        // POST: api/RoleFormActions/CheckRoleCanViewForm
        [HttpPost]
        [Route("CheckRoleCanViewForm")]
        public ActionResult CheckRoleCanViewForm([FromBody] RoleFormActionModel value)
        {
            var CanViewModule = _roleFormActionService.CheckRoleCanViewForm(value);

            return Ok(CanViewModule);
        }

        // POST: api/RoleFormActions/Details
        [HttpPost]
        [Route("Details")]
        public async Task<ActionResult> Get([FromBody] RoleFormActionModel value)
        {
            var item = await _roleFormActionService.GetById(value.RoleId, value.FormActionId);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(new { data = item });
        }

        // POST: api/RoleFormActions
        [HttpPost]
        public ActionResult Post([FromBody] RoleFormArrayAction value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _roleFormActionService.AddRange(value);

            return Ok(new { data = value });
        }        

        // PUT: api/RoleFormActions
        [HttpPut()]
        public async Task<ActionResult> Remove([FromBody] RoleFormActionModel value)
        {
            var existingItem = await _roleFormActionService.GetById(value.RoleId, value.FormActionId);
            if (existingItem == null)
            {
                return NotFound();
            }

            _roleFormActionService.Remove(existingItem);

            return Ok(new { data = value });
        }
    }
}

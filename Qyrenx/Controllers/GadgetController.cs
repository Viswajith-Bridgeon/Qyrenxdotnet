using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.DTOs.GadgetDtos;
using Qyrenx.Business.Services.GadgetServices;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GadgetController : ControllerBase
    {

        private readonly IGadgetSerives _gadgetSerives;

        public GadgetController(IGadgetSerives gadgetSerives)
        {
            _gadgetSerives = gadgetSerives;
        }

        [Authorize(Roles ="User")]
        [HttpPost("AddGadget")]

        public async Task<IActionResult> Addgadget([FromForm] GadgetAddDto dto)
        {
            try
            {
                var userId = Guid.Parse(HttpContext.Items["Id"].ToString());
                var gadget = await _gadgetSerives.Addgadget(userId, dto);
                if (!gadget)
                {
                    return NotFound(gadget);
                }
                return Ok(gadget);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

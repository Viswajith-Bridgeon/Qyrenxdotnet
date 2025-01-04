using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.DTOs.GadgetDtos;
using Qyrenx.Business.Services.GadgetServices;
using Qyrenx.Dataccess.ApiResponses;

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

        public async Task<IActionResult> Addgadget([FromForm] GadgetAddDto dto,IFormFile img)
        {
            try
            {
                var userId = Guid.Parse(HttpContext.Items["Id"].ToString());
                var gadget = await _gadgetSerives.Addgadget(userId, dto,img);
                if (!gadget)
                {
                    var r = new ApiResponse<bool>(402, "error occured", gadget);
                    return Ok(r);
                }
                var res= new ApiResponse<bool>(200,"successfully Request for services",gadget);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "sewrver error", null, ex.Message);
                return StatusCode(500, r);
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.DTOs.StatusDtos;
using Qyrenx.Business.Services.StatusServices;
using Qyrenx.Dataccess.ApiResponses;
using Qyrenx.Dataccess.Models.Entities;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {

        private readonly IStatusServices _statusServices;

        public StatusController(IStatusServices statusServices) 
        {
           _statusServices = statusServices;
            
        }



        [HttpGet("ViewStatus")]
        public async Task<IActionResult> ViewPickupStatus(Guid pid)
        {
            try
            {
                var res = await _statusServices.GetStatuses(pid);
                var r = new ApiResponse<ICollection<StatusViewDto>>(200, "successfully reseted password", res);
                return Ok(r);
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
                return StatusCode(500, r);
            }

        }
    }
}

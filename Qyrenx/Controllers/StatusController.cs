using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.DTOs.StatusDtos;
using Qyrenx.Business.Services.StatusServices;
using Qyrenx.Dataccess.ApiResponses;
using Qyrenx.Dataccess.DbAccess.Pickuprep;
using Qyrenx.Dataccess.Models.Entities;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {

        private readonly IStatusServices _statusServices;
        private readonly IpickupsRepo _pickupsRepo;

        public StatusController(IStatusServices statusServices, IpickupsRepo pickupsRepo)
        {
           _statusServices = statusServices;
            _pickupsRepo = pickupsRepo;
            
        }



        [HttpGet("ViewStatus")]
        public async Task<IActionResult> ViewPickupStatus(Guid pid)
        {
            try
            {
                var pick=await _pickupsRepo.GetPickupById(pid);
                if (pick == null)
                {
                    var re = new ApiResponse<bool>(400, " invalid pickupid", false);
                    return BadRequest(re);
                }
                var res = await _statusServices.GetStatuses(pid);
                var r = new ApiResponse<ICollection<StatusViewDto>>(200, "Fetched Status", res);
                return Ok(r);
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
                return StatusCode(500, r);
            }
        }

        [HttpGet("AddStatus")]
        public async Task<IActionResult> AddStatus(Guid pid,string status)
        {
            try
            {
                var res = await _statusServices.AddStatus(pid, status);
                 if(!res)
                {
                    var re = new ApiResponse<bool>(404, "invalid pickupid", res);
                    return NotFound(re);
                }
                var r = new ApiResponse<bool>(200, "added status", res);
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

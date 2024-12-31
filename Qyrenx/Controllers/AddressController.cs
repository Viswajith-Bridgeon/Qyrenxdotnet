using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.DTOs.AddressDtos;
using Qyrenx.Business.Services.AddressServices;
using Qyrenx.Dataccess.ApiResponses;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressServices _addressServices;

        public AddressController(IAddressServices addressServices)
        {
            _addressServices = addressServices;
        }


        [Authorize(Roles = "User")]
        [HttpPost("AddAddress")]
        public async Task<IActionResult> AddAddress([FromForm] AddressAddDto dto)
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["Id"].ToString());

                var address = await _addressServices.addAddress(usedId, dto);

                if (!address)
                {
                    var res = new ApiResponse<bool>(404, "Invalid Id", address);
                    return NotFound(res);
                }
                var re = new ApiResponse<bool>(200, "successfully added", address);
                return Ok(re);

            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "sewrver error", null, ex.Message);
                return StatusCode(500, r);
            }
        }




        [Authorize(Roles = "User")]
        [HttpGet("ViewAddress")]
        public async Task<IActionResult> ViewAddress()
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["Id"].ToString());
                var address = await _addressServices.ViewAddress(usedId);
                var res = new ApiResponse<List<AddressViewDto>>(200, "fetched address", address);
                return Ok(res);

            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "sewrver error", null, ex.Message);
                return StatusCode(500, r);
            }
        }



        [Authorize(Roles = "User")]
        [HttpPut("UpdateAddress{AddressId}")]
        public async Task<IActionResult> updateAddress(Guid AddressId, [FromForm] AddressAddDto dto)
        {
            try
            {
                var address = await _addressServices.UpdateAddrsss(AddressId, dto);
                if (!address)
                {
                    var r = new ApiResponse<bool>(400, "invalid id", address);
                    return BadRequest(r);
                }
                var res = new ApiResponse<bool>(200, "updated succesfully", address);
                return Ok(res);

            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "sewrver error", null, ex.Message);
                return StatusCode(500, r);
            }
        }


        [Authorize(Roles = "User")]
        [HttpPatch("deleteAddress{AddressId}")]
        public async Task<IActionResult> updateAddress(Guid AddressId)
        {
            try
            {
                var address = await _addressServices.DeleteAddrsss(AddressId);
                if (!address)
                {
                    var r = new ApiResponse<bool>(404, "invalid id", address);
                    return NotFound(r);
                }
                var res = new ApiResponse<bool>(200, " succesfully deleted", address);
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

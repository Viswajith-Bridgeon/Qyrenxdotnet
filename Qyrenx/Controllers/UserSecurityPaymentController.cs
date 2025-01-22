using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.DTOs.UserSecurityPaymentDto;
using Qyrenx.Business.Services.UserSecurityPay;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSecurityPaymentController : ControllerBase
    {
        private readonly IUserSecurityPaymentService _userSecurityPaymentService;
        public UserSecurityPaymentController(IUserSecurityPaymentService userSecurityPaymentService)
        {
            _userSecurityPaymentService = userSecurityPaymentService;
        }

        [HttpPost("ordercreation")]
        [Authorize]
        public async Task<ActionResult> PaymentCreation(long price)
        {
            try
            {
                if (price <= 0)
                {
                    return BadRequest("Price should be greater than zero");
                }
                var order = await _userSecurityPaymentService.RazorOrderCreate(price);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }

        [HttpPost("paymentvalidation")]
        [Authorize]
        public ActionResult PaymentValidate(UserSecurityRazorDto razorDto)
        {
            try
            {
                if (razorDto == null)
                {
                    return BadRequest("Razorepay detailes are undefined");
                }
                var valid = _userSecurityPaymentService.PaymentVerify(razorDto);
                return Ok(valid);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }

        }


        //[HttpPost("placeorder")]
        //[Authorize]
        //public async Task<ActionResult> PaymentSeting(UserSecurityInputDto inputorderDto)
        //{
        //    try
        //    {
        //        var userId = Guid.Parse(HttpContext.Items["Id"].ToString());
        //        var createorder = await _userSecurityPaymentService.CreateOrder(userId, inputorderDto);
        //        return Ok(createorder);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.InnerException.Message);
        //    }
        //}
        [HttpPost("placeorder")]
        [Authorize]
        public async Task<ActionResult> PaymentSeting(UserSecurityInputDto inputorderDto)
        {
            try
            {
                // Check if "Id" exists in HttpContext.Items and is not null
                if (HttpContext.Items["Id"] == null)
                {
                    return BadRequest("User ID not found.");
                }

                var userId = Guid.Parse(HttpContext.Items["Id"].ToString());

                // Ensure the service call handles any potential errors
                var createOrderResult = await _userSecurityPaymentService.CreateOrder(userId, inputorderDto);

                // Assuming CreateOrder returns a boolean or some result object
                if (createOrderResult)
                {
                    return Ok("Order placed successfully.");
                }
                else
                {
                    return BadRequest("Failed to place the order.");
                }
            }
            catch (Exception ex)
            {
                // Log the full exception message for debugging
                return StatusCode(500, ex.Message);  // Use ex.Message instead of ex.InnerException.Message
            }
        }

    }
}

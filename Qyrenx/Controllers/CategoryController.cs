using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.DTOs.CategoryDto;
using Qyrenx.Business.Services.CategoryServices;
using Qyrenx.Dataccess.Models.Entities;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<Category>> Get()
        {
            try
            {
                var res = await _categoryService.GetCategory();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult>AddCategory([FromForm]string name,string dis,IFormFile img)
        {
            try
            {
                var res = await _categoryService.AddCategory(name,dis, img);
                if (res)
                    return Ok("Category Added Successfully");
                return BadRequest("Already Exist");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCategory([FromForm] Guid id, string name, IFormFile img)
        {
            try
            {
                var res = await _categoryService.UpdateCategory(id,name, img);
                if (res)
                    return Ok("Category Updated Successfully");
                return BadRequest("Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCategory([FromForm] Guid id)
        {
            try
            {
                var res = await _categoryService.DeleteCategory(id);
                if (res)
                    return Ok("Category Deleted Successfully");
                return BadRequest("Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

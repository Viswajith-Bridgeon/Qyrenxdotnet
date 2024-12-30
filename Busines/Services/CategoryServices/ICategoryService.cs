using Microsoft.AspNetCore.Http;
using Qyrenx.Business.DTOs.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<bool>AddCategory(string name,IFormFile image);
        Task<bool> UpdateCategory(Guid id, string name, IFormFile image);
        Task<bool> DeleteCategory(Guid id);

        Task<IEnumerable<CategoryAddDto>> GetCategory();

    }
}

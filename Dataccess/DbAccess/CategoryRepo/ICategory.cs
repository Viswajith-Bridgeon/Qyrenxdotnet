using Microsoft.AspNetCore.Http;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.CategoryRepo
{
    public interface ICategory
    {
        Task<bool> AddCategory(string name, string dis, string Icloud_image);
        Task<bool> UpdateCategory(Guid id, string name, string Icloud_image);
        Task<bool> DeleteCategory(Guid id);
        Task<IEnumerable<Category>> GetCategory();
        Task<Category> GetCategoryById(Guid id);
    }
}

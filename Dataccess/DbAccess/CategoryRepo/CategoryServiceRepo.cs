using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.CategoryRepo
{
    public class CategoryServiceRepo:ICategory
    {
        private readonly QyrenxContext _context;
        public CategoryServiceRepo(QyrenxContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetCategory()
        {
            try
            {
                var res = await _context.Categories.ToListAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<bool> AddCategory(string name, string dis, string Icloud_image)
        {
            try
            {
                var exist = _context.Categories.Where(c => c.CategoryName.ToLower() == name.ToLower());
                if (!exist.Any())
                {
                    var category = new Category
                    {
                        CategoryName = name,
                        Image = Icloud_image,
                        CategoryDescription = dis,
                    };
                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            try
            {
               
                var exist = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                if (exist != null)
                {
                    exist.IsDelete = true;
                    exist.UpdatedOn = DateTime.UtcNow;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }



        public async Task<bool> UpdateCategory(Guid id, string name, string Icloud_image)
        {
            try
            {
                var exist =await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                if (exist != null)
                {
                    if (Icloud_image != null && name != null)
                    {
                        exist.Image = Icloud_image;
                        exist.CategoryName = name;
                        _context.Categories.Update(exist);
                        _context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            try
            {
                var data=await _context.Categories.FirstOrDefaultAsync(c=>c.CategoryId == id);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Qyrenx.Business.DTOs.CategoryDto;
using Qyrenx.Business.Services.CloudinaryService;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly QyrenxContext _context;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        public CategoryService(QyrenxContext context,ICloudinaryService cloudinaryService,IMapper mapper)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryAddDto>> GetCategory()
        {
            try
            {
                var res =  _context.Categories.ToList();  
                var cat = _mapper.Map<IEnumerable<CategoryAddDto>>(res); 
                return cat;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<bool> AddCategory(string name,IFormFile image)
        {
            try
            {
                var exist=_context.Categories.Where(c=>c.CategoryName.ToLower()==name.ToLower());
                if (!exist.Any())
                {
                    var img = await _cloudinaryService.UploadDocumentAsync(image);
                    var category = new CategoryAddDto
                    {
                        CategoryName = name,
                        Image = img,
                    };
                    var cat = _mapper.Map<Category>(category);
                    await _context.Categories.AddAsync(cat);
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
                var exist=  _context.Categories.FirstOrDefault(c=>c.CategoryId==id);
                if(exist!=null)
                {
                    _context.Categories.Remove(exist);
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

        

        public async Task<bool> UpdateCategory(Guid id, string name, IFormFile image)
        {
            try
            {
                var exist =  _context.Categories.FirstOrDefault(c => c.CategoryId == id);
                if (exist != null)
                {
                    if (image != null && name != null)
                    {
                        var img = await _cloudinaryService.UploadDocumentAsync(image);
                        exist.Image = img;
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


    }
}

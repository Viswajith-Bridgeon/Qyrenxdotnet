using AutoMapper;
using Microsoft.AspNetCore.Http;
using Qyrenx.Business.DTOs.CategoryDto;
using Qyrenx.Business.Services.CloudinaryService;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qyrenx.Dataccess.DbAccess;
using Qyrenx.Dataccess.DbAccess.CategoryRepo;

namespace Qyrenx.Business.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly QyrenxContext _context;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        private readonly ICategory _category; 
        public CategoryService(QyrenxContext context,ICloudinaryService cloudinaryService,IMapper mapper, ICategory category)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
            _category = category;
        }

        public async Task<IEnumerable<CategoryViewDto>> GetCategory()
        {
            try
            {
                var categories=await _category.GetCategory();
                var res=_mapper.Map<IEnumerable<CategoryViewDto>>(categories);
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<bool> AddCategory(string name,string dis, IFormFile image)
        {
            try
            {
              
                var img = await _cloudinaryService.UploadDocumentAsync(image);
                var res = await _category.AddCategory(name, dis, img);
                if(res)
                    return true;
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
               var res= await _category.DeleteCategory(id);
                if(res)
                {
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
                var img = await _cloudinaryService.UploadDocumentAsync(image);
                var res =await _category.UpdateCategory(id, name,img);
                if (res)
                {
                   
                        return true;
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

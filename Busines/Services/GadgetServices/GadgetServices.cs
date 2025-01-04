using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Business.DTOs.GadgetDtos;
using Qyrenx.Business.Services.CloudinaryService;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.DbAccess;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.GadgetServices
{
    public class GadgetServices:IGadgetSerives
    {
        private readonly QyrenxContext _qyrenxContext;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IDbAccess _dbAccess;


        public GadgetServices(QyrenxContext qyrenxContext,IMapper mapper,ICloudinaryService cloudinaryService,IDbAccess dbAccess)
            {
          _qyrenxContext = qyrenxContext;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _dbAccess = dbAccess;
        }

        public async Task<bool> Addgadget(Guid id, GadgetAddDto dto, IFormFile img)
        {
            try
            {
                var data = await _dbAccess.GetAllUsers();
                var user = data.FirstOrDefault(e => e.Id == id);
                if (user == null)
                {
                    return false;
                }
                var gadgetimg=await _cloudinaryService.UploadDocumentAsync(img);
                var g = new Gadget
                {
                    UserId = id,
                    CategoryId = dto.CategoryId,
                    GadgetName = dto.GadgetName,
                    Image = gadgetimg,
                    Description = dto.Description,
                    AddressId = dto.AddressId,
                };
                g.CreatedOn=DateTime.Now;
                g.CreatedBy=user.Name;
                _qyrenxContext.Gadgets.Add(g);
                await _qyrenxContext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

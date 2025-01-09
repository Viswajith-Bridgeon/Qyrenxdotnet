using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Business.DTOs.GadgetDtos;
using Qyrenx.Business.Services.CloudinaryService;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.DbAccess;
using Qyrenx.Dataccess.DbAccess.GadgetRepo;
using Qyrenx.Dataccess.DbAccess.UserRepo;
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
        private readonly IuserRepo _userRepo;
        private readonly IgadgetRepo _gadgetRepo;
        private readonly IDbAccess _dbAccess;


        public GadgetServices(QyrenxContext qyrenxContext,IMapper mapper,ICloudinaryService cloudinaryService,IDbAccess dbAccess,IuserRepo userRepo,IgadgetRepo gadgetRepo)
            {
          _qyrenxContext = qyrenxContext;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _dbAccess = dbAccess;
            _userRepo = userRepo;   
            _gadgetRepo = gadgetRepo;
        }

        public async Task<bool> Addgadget(Guid id, GadgetAddDto dto, IFormFile img)
        {
            try
            {
                var data = await _dbAccess.GetAllUsers();
                var user = await _userRepo.GetUserById(id);
                if (user == null)
                {
                    return false;
                }
                var gadgetimg = await _cloudinaryService.UploadDocumentAsync(img);
                var dataa = _mapper.Map<Gadget>(dto);
                var value=await _gadgetRepo.Addgadget(id, dataa,user,gadgetimg);
                if (value == true) 
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Qyrenx.Business.DTOs.GadgetDtos;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.GadgetServices
{
    public interface IGadgetSerives
    {

        Task<bool> Addgadget(Guid id,GadgetAddDto dto,IFormFile img);
        Task<List<GadgetviewDto>> GetAll();
    }
}

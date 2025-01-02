using Qyrenx.Business.DTOs.GadgetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.GadgetServices
{
    public interface IGadgetSerives
    {

        Task<bool> Addgadget(Guid id,GadgetAddDto dto);
    }
}

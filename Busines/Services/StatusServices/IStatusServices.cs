using Qyrenx.Business.DTOs.StatusDtos;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.StatusServices
{
    public interface IStatusServices
    {

        Task<ICollection<StatusViewDto>> GetStatuses(Guid pid);
    }
}

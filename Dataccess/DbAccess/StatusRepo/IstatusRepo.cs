using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.StatusRepo
{
    public interface IstatusRepo
    {
        Task<List<Status>> GetAllStatus();
        Task<List<Status>> GetStatusByPickId(Guid id);

    }
}

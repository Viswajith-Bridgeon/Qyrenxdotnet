using Microsoft.AspNetCore.Http;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.GadgetRepo
{
    public interface IgadgetRepo
    {
        Task<bool> Addgadget(Guid id, Gadget dto, User user, string gadgetimg);
        Task<List<Gadget>> Getgadgets();
        Task<Gadget> GetordergadgetsById(Guid id);
    }
}

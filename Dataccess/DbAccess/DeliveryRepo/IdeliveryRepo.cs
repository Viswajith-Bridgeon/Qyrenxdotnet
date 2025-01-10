using Microsoft.AspNetCore.Http;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.DeliveryRepo
{
    public interface IdeliveryRepo
    {
        Task <string>Register(DeliveryPerson person,string Icloudinary_Service);
        Task<IEnumerable<DeliveryPerson>> GetAllDeliveryPeresons();
        Task<DeliveryPerson> GetDeliveryPeresonById(Guid Id);
        Task<bool> BlockOrUnblock(Guid id);
        Task<bool> verify(Guid id);
        Task<List<DeliveryPersonOnline>> GetAllDeliveryPersonOnline();
        Task<List<DeliveryPersonOnline>> GetActiveDeliveryPersons();


    }
}

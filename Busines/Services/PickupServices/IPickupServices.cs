using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.DTOs;
using Qyrenx.Business.DTOs.Deliverypersons;
using Qyrenx.Business.DTOs.VendorDtos;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.PickupServices
{
    public interface IPickupServices
    {
        Task<List<PickupDto>> GetPickupsDeliveryBoys(Guid id);
        Task<bool> VerifyPickup(Guid id, Guid userid);
        Task<LatLong> LatLongOfUser(Guid id);
        Task<bool> SendFormToUser(Guid ven_id, VendorCostDto details);



    }
}

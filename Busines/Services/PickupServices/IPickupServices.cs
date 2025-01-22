using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.DTOs;
using Qyrenx.Business.DTOs.Deliverypersons;
using Qyrenx.Business.DTOs.PickUpDtos;
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
        Task<List<PickupDeliveryDto>> GetPickupsDeliveryBoys(Guid id);
        Task<string> VerifyPickup(Guid id, Guid userid);
        Task<LatLong> LatLongOfUser(Guid id);
        Task<bool> SendFormToUser(Guid ven_id, VendorCostDto details);

        Task<string>  pickupVerificationofUser(Guid id,string otp);

        Task<List<PickupVendorDto>> GetPickupsVendor(Guid id);


        Task<string> VerifyPickupByDeliveryboyToVendor(Guid id, Guid userid);

        Task<bool> pickupVerificationofVendor(Guid pid, string otp);

        Task<VendorCostView> GetSeviceDetialsByPickup(Guid userid, Guid pickupid);

        Task<ICollection<PickUpDto>> GetPickupsUserId(Guid id);


        Task<string> UserApproveService(Guid Vc_id);

        Task<ICollection<PickUpDto>> UserApprovedService(Guid Vcid);
    }
}

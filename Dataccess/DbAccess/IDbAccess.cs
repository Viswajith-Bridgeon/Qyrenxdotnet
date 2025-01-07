using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess
{
    public interface IDbAccess
    {
        Task<List<User>> GetAllUsers();
        Task<List<Vendor>> GetAllVendor();
        Task<List<DeliveryPerson>> GetAllDeliveryPerson();
        Task<List<DeliveryPersonOnline>> GetAllDeliveryPersonOnline();
        Task<List<Gadget>> GetAllGadgets();
        Task<List<OrderGadget>> GetAllOrdersGadgets();
        Task<List<PaymentToUser>> GetAllPaymentToUser();
        Task<List<Pickup>> GetAllPickups();
        Task<List<Status>> GetAllStatus();
        Task<List<VendorAddress>> GetAllVendorAddresses();
        Task<List<Address>> GetAllAddressAddresses();
        Task<List<UserSecurityPayment>> GetAllUserSecurityPayment();
        Task<List<Category>> GetAllCategories();
        Task<List<VendorCategory>> GetAllVendorCategories();
        Task<List<VendorOnline>> GetAllVendorOnline();
    }
}

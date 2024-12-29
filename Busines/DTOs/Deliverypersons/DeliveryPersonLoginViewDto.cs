using System.ComponentModel.DataAnnotations;

namespace Qyrenx.Business.Models.DTOs.Deliverypersons
{
    public class DeliveryPersonLoginViewDto
    {
        public string DeliveryPersonName { get; set; }
        public Guid id { get; set; }
        public string token { get; set; }
        public string Error { get; set; }
    }
}

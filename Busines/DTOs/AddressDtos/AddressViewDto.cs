using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs.AddressDtos
{
    public class AddressViewDto 
    {
        public Guid Id { get; set; }
        public string House { get; set; }
        public string City { get; set; }
        public string LandMark { get; set; }
        public string PostalCode { get; set; }
    }
}

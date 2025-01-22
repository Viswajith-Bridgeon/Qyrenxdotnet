using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs.AddressDtos
{
    public  class AddressAddDto
    {
        [Required(ErrorMessage="House name is required")]
        public string? House { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "LandMark is required")]
        public string? LandMark { get; set; }

        [Required(ErrorMessage = "postalCode is required")]
        public string? PostalCode { get; set; }
    }
}

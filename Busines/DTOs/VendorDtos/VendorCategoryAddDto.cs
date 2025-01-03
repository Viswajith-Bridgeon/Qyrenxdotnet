using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs.VendorDtos
{
    public class VendorCategoryAddDto
    {
        [Required]
        public Guid VendorId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}

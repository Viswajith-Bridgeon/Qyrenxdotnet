using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs.GadgetDtos
{
    public class GadgetAddDto
    {
        public Guid CategoryId { get; set; }
        public string GadgetName { get; set; }
        public string Image { get; set; }
        public Guid AddressId { get; set; }
        public string Description { get; set; }
    }
}

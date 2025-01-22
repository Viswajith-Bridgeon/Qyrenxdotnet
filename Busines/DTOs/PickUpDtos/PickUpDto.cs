using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs.PickUpDtos
{
    public class PickUpDto
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string GadgetName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }


    }
}

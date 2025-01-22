using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs.StatusDtos
{
    public class StatusViewDto
    {
        public Guid Id { get; set; }
        public Guid PickupId { get; set; }
        public string Statuss { get; set; }

        public DateTime date { get; set; }
    }
}

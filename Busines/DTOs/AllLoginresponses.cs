using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs
{
    public class AllLoginresponses
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? refreshToken { get; set; }
        public string? Error { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAPI.Application.Dtos.OrderDto
{
    public static class OrderStatus
    {
        public const string Hazirlaniyor = "HAZIRLANIYOR";
        public const string Hazir = "HAZIR";
        public const string TeslimEdildi = "TESLIM_EDILDI";
        public const string IptalEdildi = "IPTAL_EDILDI";
    }
}

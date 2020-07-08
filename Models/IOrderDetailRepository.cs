using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> AllOrderDetails { get; }

        IEnumerable<OrderDetail> OrderDetailsForCurrentUser(IServiceProvider services);
    }
}

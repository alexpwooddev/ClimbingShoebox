using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly AppDbContext appDbContext;

        public OrderDetailRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<OrderDetail> AllOrderDetails
        {
            get
            {
                return appDbContext.OrderDetails;
            }
        }

        public IEnumerable<OrderDetail> OrderDetailsForCurrentUser(IServiceProvider services)
        {
            var userId = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return appDbContext.OrderDetails.Where(o => o.ApplicationUserId == userId).Include(o => o.Shoe).OrderBy(o => o.OrderId);
   
        }
    }
}

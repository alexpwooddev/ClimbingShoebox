using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IServiceProvider services;
        private readonly ShoppingCart shoppingCart;

        public OrderRepository(AppDbContext appDbContext, IServiceProvider services, ShoppingCart shoppingCart)
        {
            this.appDbContext = appDbContext;
            this.services = services;
            this.shoppingCart = shoppingCart;
        }
        
        public void CreateOrder(Order order)
        {
            var userId = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            order.OrderPlaced = DateTime.Now;

            var shoppingCartItems = shoppingCart.ShoppingCartItems;
            order.OrderTotal = shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    ShoeId = shoppingCartItem.Shoe.ShoeId,
                    Price = shoppingCartItem.Shoe.Price,
                    ApplicationUserId = userId
                };

                order.OrderDetails.Add(orderDetail);
            }

            appDbContext.Orders.Add(order);

            appDbContext.SaveChanges();
        }
    }
}

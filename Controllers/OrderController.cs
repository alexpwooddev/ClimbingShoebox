using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClimbingShoebox.Models;
using ClimbingShoebox.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClimbingShoebox.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IServiceProvider services;
        private readonly ShoppingCart shoppingCart;

        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, 
            IServiceProvider services, ShoppingCart shoppingCart)
        {
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.services = services;
            this.shoppingCart = shoppingCart;
        }
        
        public IActionResult Checkout()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                TempData["emptyCartMessage"] = "Your cart is empty, add some shoes first";
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                return View();
            }

            
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some shoes first");
            }

            if (ModelState.IsValid)
            {
                orderRepository.CreateOrder(order);
                shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order.";
            return View();
        }

        public IActionResult PastOrders()
        {
            //need to get OrderDetails for the current user and order them by OrderId
            List<OrderDetail> currentUserOrderDetails = orderDetailRepository.OrderDetailsForCurrentUser(services).ToList();

            //need to present these in some view to the user
            return View(new PastOrdersViewModel
            {
                CurrentUserOrders = currentUserOrderDetails
            });

            
        }
    }
}

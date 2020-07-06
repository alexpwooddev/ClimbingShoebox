using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClimbingShoebox.Models;
using ClimbingShoebox.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClimbingShoebox.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoeRepository shoeRepository;
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartController(IShoeRepository shoeRepository, ShoppingCart shoppingCart)
        {
            this.shoeRepository = shoeRepository;
            this.shoppingCart = shoppingCart;
        }


        public ViewResult Index()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;
            string message;

            if (TempData.ContainsKey("emptyCartMessage"))
            {
                message = TempData["emptyCartMessage"].ToString();
            }
            else
            {
                message = null;
            }

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal(),
                tempMessage = message
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int shoeId)
        {
            var selectedShoe = shoeRepository.AllShoes.FirstOrDefault(s => s.ShoeId == shoeId);

            if (selectedShoe != null)
            {
                shoppingCart.AddToCart(selectedShoe, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int shoeId)
        {
            var selectedShoe = shoeRepository.AllShoes.FirstOrDefault(s => s.ShoeId == shoeId);

            if (selectedShoe != null)
            {
                shoppingCart.RemoveFromCart(selectedShoe);
            }
            return RedirectToAction("Index");
        }


    }
}

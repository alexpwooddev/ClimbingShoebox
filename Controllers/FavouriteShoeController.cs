using ClimbingShoebox.Models;
using ClimbingShoebox.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingShoebox.Controllers
{
    public class FavouriteShoeController : Controller
    {
        private readonly FavouritesCollection favouritesCollection;
        private readonly IServiceProvider services;

        public FavouriteShoeController(FavouritesCollection favouritesCollection, IServiceProvider services)
        {
            this.favouritesCollection = favouritesCollection;
            this.services = services;
        }


        public IActionResult ListFavouriteShoes()
        {
            var items = favouritesCollection.GetCollectionItems(services);
            favouritesCollection.FavouritesCollectionItems = items;
            string message;

            if (TempData.ContainsKey("favouritedMessage"))
            {
                message = TempData["favouritedMessage"].ToString();
            }
            else
            {
                message = null;
            }


            if (items == null)
            {
                return RedirectToAction(nameof(NoFavourites));
            }
            else
            {
                return View(new FavouritesCollectionViewModel
                {
                    FavouritesCollection = favouritesCollection,
                    tempMessage = message
                });
            }

        }


        public IActionResult NoFavourites()
        {
            return View();
        }


        public IActionResult AddToFavourite(int shoeId)
        {
            favouritesCollection.AddToCollection(services, shoeId);

            return RedirectToAction(nameof(ListFavouriteShoes));
        }


        public IActionResult RemoveFromFavourite(int shoeId)
        {
            favouritesCollection.RemoveFromCollection(services, shoeId);

            TempData["favouritedMessage"] = "Shoes were removed from your favourites";

            return RedirectToAction(nameof(ListFavouriteShoes));
        }


        public IActionResult AddCommentToFavourite(int favouriteCollectionItemId)
        {
            string comment = Request.Form["Comment"];

            favouritesCollection.SaveComment(services, favouriteCollectionItemId, comment);

            return RedirectToAction(nameof(ListFavouriteShoes));
        }
    }
}

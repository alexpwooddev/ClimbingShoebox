using ClimbingShoebox.Models;
using ClimbingShoebox.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Components
{
    public class FavouritesSummary : ViewComponent
    {
        private readonly FavouritesCollection favouritesCollection;

        public FavouritesSummary(FavouritesCollection favouritesCollection)
        {
            this.favouritesCollection = favouritesCollection;
        }

        public IViewComponentResult Invoke()
        {




            var items = favouritesCollection.GetCollectionItems();
            favouritesCollection.FavouritesCollectionItems = items;

            var favouritesCollectionViewModel = new FavouritesCollectionViewModel
            {
                FavouritesCollection = favouritesCollection
            };
            return View(favouritesCollectionViewModel);
        }
    }
}

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
        private readonly IServiceProvider services;

        public FavouritesSummary(FavouritesCollection favouritesCollection, IServiceProvider services)
        {
            this.favouritesCollection = favouritesCollection;
            this.services = services;
        }

        public IViewComponentResult Invoke()
        {

            var items = favouritesCollection.GetCollectionItems(services);
            favouritesCollection.FavouritesCollectionItems = items;

            var favouritesCollectionViewModel = new FavouritesCollectionViewModel
            {
                FavouritesCollection = favouritesCollection
            };
            return View(favouritesCollectionViewModel);
        }
    }
}

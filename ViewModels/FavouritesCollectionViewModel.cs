using ClimbingShoebox.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.ViewModels
{
    public class FavouritesCollectionViewModel
    {
        public FavouritesCollection FavouritesCollection { get; set; }

        [TempData]
        public string tempMessage { get; set; }


    }
}

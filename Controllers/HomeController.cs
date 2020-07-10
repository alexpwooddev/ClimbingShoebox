using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClimbingShoebox.Models;
using ClimbingShoebox.ViewModels;

namespace ClimbingShoebox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShoeRepository shoeRepository;

        public HomeController(IShoeRepository shoeRepository)
        {
            this.shoeRepository = shoeRepository;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                FeaturedShoe = shoeRepository.FeaturedShoe
            };

            return View(homeViewModel);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

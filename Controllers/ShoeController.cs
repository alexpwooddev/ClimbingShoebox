using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClimbingShoebox.Models;
using ClimbingShoebox.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClimbingShoebox.Controllers
{
    public class ShoeController : Controller
    {
        private readonly IShoeRepository shoeRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IBrandRepository brandRepository;

        public ShoeController(IShoeRepository shoeRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository)
        {
            this.shoeRepository = shoeRepository;
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
        }
        
        public IActionResult List()
        {
            ShoesListViewModel shoesListViewModel = new ShoesListViewModel();
            shoesListViewModel.Shoes = shoeRepository.AllShoes;
                
            return View(shoesListViewModel);
        }
    }
}

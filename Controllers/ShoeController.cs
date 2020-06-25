using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
        
        public IActionResult List(string categoryOrBrand)
        {
            IEnumerable<Shoe> shoes;
            string currentCategoryOrBrand;

            if (string.IsNullOrEmpty(categoryOrBrand))
            {
                shoes = shoeRepository.AllShoes.OrderBy(s => s.ShoeId);
                currentCategoryOrBrand = "All shoes";
            }
            else
            {
                if (shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId == null)
                {
                    shoes = shoeRepository.AllShoes.Where(s => s.Brand.BrandName == categoryOrBrand);
                }
                else
                {
                    shoes = shoeRepository.AllShoes.Where(s => s.Category.CategoryName == categoryOrBrand).OrderBy(s => s.ShoeId);
                }
                
                if (categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == categoryOrBrand)?.CategoryName == null)
                {
                    currentCategoryOrBrand = brandRepository.AllBrands.FirstOrDefault(b => b.BrandName == categoryOrBrand)?.BrandName;
                }
                else
                {
                    currentCategoryOrBrand = categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == categoryOrBrand)?.CategoryName;
                }                            
            }

            return View(new ShoesListViewModel
            {
                Shoes = shoes,
                CurrentCategoryOrBrand = currentCategoryOrBrand
            });


            
        }

        public IActionResult Details(int shoeId)
        {
            var shoe = shoeRepository.GetShoebyId(shoeId);
            if(shoe == null)
            {
                return NotFound();
            }
            return View(shoe);
        }
    }
}

using ClimbingShoebox.Models;
using ClimbingShoebox.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Components
{
    public class CategoryAndBrandMenu : ViewComponent
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IBrandRepository brandRepository;

        public CategoryAndBrandMenu(ICategoryRepository categoryRepository, IBrandRepository brandRepository)
        {
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categoryAndBrandViewModel = new CategoryAndBrandViewModel
            {
                Categories = categoryRepository.AllCategories.OrderBy(c => c.CategoryName).ToList(),
                Brands = brandRepository.AllBrands.OrderBy(b => b.BrandName).ToList()
            };

            return View(categoryAndBrandViewModel);
        }

    }
}

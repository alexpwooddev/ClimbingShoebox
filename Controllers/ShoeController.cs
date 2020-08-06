using ClimbingShoebox.Models;
using ClimbingShoebox.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClimbingShoebox.Controllers
{
    public class ShoeController : Controller
    {
        private readonly IShoeRepository shoeRepository;
        private readonly IServiceProvider services;
        private readonly IRatingEntryRepository ratingEntryRepository;

        private enum SortBy
        {
            descendingByPrice,
            ascendingByPrice,
            descendingByRating,
            ascendingByRating
        }

        public ShoeController(IShoeRepository shoeRepository, ICategoryRepository categoryRepository,
            IBrandRepository brandRepository, IServiceProvider services
            , IRatingEntryRepository ratingEntryRepository)
        {
            this.shoeRepository = shoeRepository;
            this.services = services;
            this.ratingEntryRepository = ratingEntryRepository;
        }


        public IActionResult ListShoes(string categoryOrBrand, string sortBy)
        {
            IEnumerable<RatingEntry> ratingEntries = ratingEntryRepository.AllRatings;
            List<RatedShoe> ratedShoes = new List<RatedShoe>();
            ratedShoes = ShoeRatingController.CreateRatedShoeList(shoeRepository.AllShoes, ratingEntries, ratedShoes).ToList();                   
            
            bool NoCategoryOrBrandNorAscendingOrDescendingSelected = string.IsNullOrEmpty(categoryOrBrand) && string.IsNullOrEmpty(sortBy);
            bool AscendingOrDescendingButNoCategoryOrBrandSelected = string.IsNullOrEmpty(categoryOrBrand) && !string.IsNullOrEmpty(sortBy);
            
            if (NoCategoryOrBrandNorAscendingOrDescendingSelected) //i.e. just all shoes
            {
                return View(new ShoesListViewModel
                {
                    CurrentCategoryOrBrand = categoryOrBrand,
                    RatedShoes = ratedShoes.OrderBy(r => r.Shoe.ShoeId).ToList()
            });
            }

            else if (AscendingOrDescendingButNoCategoryOrBrandSelected)
            {
                SortBy sortByEnum = SortByStringToEnum(sortBy);
                return View(new ShoesListViewModel
                {
                    CurrentCategoryOrBrand = categoryOrBrand,
                    RatedShoes = ShowShoesAscendingOrDescendingWithNoCategoryOrBrand(ratedShoes, sortByEnum)
            });
            }

            else
            {             
                return View(new ShoesListViewModel
                {
                    CurrentCategoryOrBrand = categoryOrBrand,
                    RatedShoes = ShowShoesWithCategoryOrBrandSelection(categoryOrBrand, ratedShoes, sortBy)
            });
            }
            
        }


        private SortBy SortByStringToEnum(string sortBy)
        {
            switch (sortBy)
            {
                case "ascendingByPrice":
                    return ShoeController.SortBy.ascendingByPrice;

                case "descendingByPrice":
                    return ShoeController.SortBy.descendingByPrice;

                case "ascendingByRating":
                    return ShoeController.SortBy.ascendingByRating;

                case "descendingByRating":
                    return ShoeController.SortBy.descendingByRating;

                default:
                    return ShoeController.SortBy.descendingByRating;
            }
        }


        private List<RatedShoe> ShowShoesWithSortByOption(List<RatedShoe> ratedShoes, string categoryOrBrand, IEnumerable<RatingEntry> ratingEntries, SortBy sortByEnum)
        {
            //categoryOrBrand is set to "Search results for ________(the query)" from the last request
            var categoryOrBrandArray = categoryOrBrand.Split(" ");
            List<string> queryIsolated = new List<string>();

            //take just the words of the query, i.e. after "Search results for"
            for (int i = 3; i < categoryOrBrandArray.Length; i++)
            {
                queryIsolated.Add(categoryOrBrandArray[i]);
            }

            string combinedQuery = "";
            foreach (string s in queryIsolated)
            {
                combinedQuery += $"{s} ";
            }

            IEnumerable<Shoe> shoes = shoeRepository.GetShoesByNameOrBrandOrCategory(combinedQuery.Trim());
            ratedShoes = ShoeRatingController.CreateRatedShoeList(shoes, ratingEntries, ratedShoes).ToList();

            if (sortByEnum == SortBy.ascendingByPrice)
            {
                ratedShoes = ratedShoes.OrderBy(s => s.Shoe.Price).ToList();
            }
            else if (sortByEnum == SortBy.descendingByPrice)
            {
                ratedShoes = ratedShoes.OrderByDescending(s => s.Shoe.Price).ToList();
            }
            else if (sortByEnum == SortBy.ascendingByRating)
            {
                ratedShoes = ratedShoes.OrderBy(s => s.OverallRating).ToList();
            }
            else
            {
                ratedShoes = ratedShoes.OrderByDescending(s => s.OverallRating).ToList();
            }

            return ratedShoes;
        }


        private List<RatedShoe> ShowShoesAscendingOrDescendingWithNoCategoryOrBrand(List<RatedShoe> ratedShoes, SortBy sortByEnum)
        {
            if (sortByEnum == SortBy.ascendingByPrice)
            {
                ratedShoes = ratedShoes.OrderBy(s => s.Shoe.Price).ToList();
            }
            else if (sortByEnum == SortBy.descendingByPrice)
            {
                ratedShoes = ratedShoes.OrderByDescending(s => s.Shoe.Price).ToList();
            }
            else if (sortByEnum == SortBy.ascendingByRating)
            {
                ratedShoes = ratedShoes.OrderBy(s => s.OverallRating).ToList();
            }
            else
            {
                ratedShoes = ratedShoes.OrderByDescending(s => s.OverallRating).ToList();
            }

            return ratedShoes;
        }


        private List<RatedShoe> ShowShoesWithCategoryOrBrandSelection(string categoryOrBrand, List<RatedShoe> ratedShoes, string sortBy)
        {
            int? matchCategoryInDb = shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId;

            //Category/Brand selected but no sort selected
            if (categoryOrBrand != "All shoes" && string.IsNullOrEmpty(sortBy))
            {
                var shoesInSelectedBrand = ratedShoes.Where(s => s.Shoe.Brand.BrandName == categoryOrBrand);
                var shoesInSelectedCategory = ratedShoes.Where(s => s.Shoe.Category.CategoryName == categoryOrBrand);

                if (matchCategoryInDb == null)
                {
                    ratedShoes = shoesInSelectedBrand.OrderBy(s => s.Shoe.ShoeId).ToList();
                }
                else
                {
                    ratedShoes = shoesInSelectedCategory.OrderBy(s => s.Shoe.ShoeId).ToList();
                }
            }

            //category/Brand selected AND sort selected
            else
            {
                ratedShoes = CategoryOrBrandAndSortSelected(categoryOrBrand, ratedShoes, sortBy, matchCategoryInDb);
            }

            return ratedShoes;
        }


        private List<RatedShoe> CategoryOrBrandAndSortSelected(string categoryOrBrand, List<RatedShoe> ratedShoes, string sortBy, int? matchCategoryInDb)
        {
            var shoesInSelectedBrand = ratedShoes.Where(s => s.Shoe.Brand.BrandName == categoryOrBrand);
            var shoesInSelectedCategory = ratedShoes.Where(s => s.Shoe.Category.CategoryName == categoryOrBrand);
            SortBy sortByEnum = SortByStringToEnum(sortBy);

            if (sortByEnum == SortBy.ascendingByPrice)
            {
                if (matchCategoryInDb == null)//i.e. this means that a brand was selected
                {
                    ratedShoes = shoesInSelectedBrand.OrderBy(s => s.Shoe.Price).ToList();
                }
                else //i.e. this means a category was selected
                {
                    ratedShoes = shoesInSelectedCategory.OrderBy(s => s.Shoe.Price).ToList();
                }
            }

            else if (sortByEnum == SortBy.descendingByPrice)
            {
                if (matchCategoryInDb == null)
                {
                    ratedShoes = shoesInSelectedBrand.OrderByDescending(s => s.Shoe.Price).ToList();
                }
                else
                {
                    ratedShoes = shoesInSelectedCategory.OrderByDescending(s => s.Shoe.Price).ToList();
                }
            }

            else if (sortByEnum == SortBy.ascendingByRating)
            {
                if (matchCategoryInDb == null)
                {
                    ratedShoes = shoesInSelectedBrand.OrderBy(s => s.OverallRating).ToList();
                }
                else
                {
                    ratedShoes = shoesInSelectedCategory.OrderBy(s => s.OverallRating).ToList();
                }
            }

            else //sortBy == descendingByRating
            {
                if (matchCategoryInDb == null)
                {
                    ratedShoes = shoesInSelectedBrand.OrderByDescending(s => s.OverallRating).ToList();
                }
                else
                {
                    ratedShoes = shoesInSelectedCategory.OrderByDescending(s => s.OverallRating).ToList();
                }
            }

            return ratedShoes;
        }


        public IActionResult ListSearchResults(string categoryOrBrand, string sortBy, string query)
        {
            IEnumerable<RatingEntry> ratingEntries = ratingEntryRepository.AllRatings;
            List<RatedShoe> ratedShoes = new List<RatedShoe>();
          
            bool isInitialSearch = !string.IsNullOrEmpty(query);
            if (isInitialSearch)
            {
                IEnumerable<Shoe> shoes = shoeRepository.GetShoesByNameOrBrandOrCategory(query);
                ratedShoes = ShoeRatingController.CreateRatedShoeList(shoes, ratingEntries, ratedShoes).ToList();

                return View("ListSearchResults", new ShoesListViewModel
                {
                    RatedShoes = ratedShoes,
                    CurrentCategoryOrBrand = $"Search results for {query}"
                });
            }

            else 
            {
                SortBy sortByEnum = SortByStringToEnum(sortBy);
                ratedShoes = ShowShoesWithSortByOption(ratedShoes, categoryOrBrand, ratingEntries, sortByEnum);

                return View("ListSearchResults", new ShoesListViewModel
                {
                    CurrentCategoryOrBrand = categoryOrBrand,
                    RatedShoes = ratedShoes
                });
            }
        }


        public IActionResult SelectedShoeDetails(int shoeId)
        {
            var shoe = shoeRepository.GetShoebyId(shoeId);
            if (shoe == null)
            {
                return NotFound();
            }
            return View(shoe);
        }

    }
}

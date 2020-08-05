﻿using ClimbingShoebox.Models;
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
        private readonly ICategoryRepository categoryRepository;
        private readonly IBrandRepository brandRepository;
        private readonly FavouritesCollection favouritesCollection;
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
            IBrandRepository brandRepository, FavouritesCollection favouritesCollection, IServiceProvider services
            , IRatingEntryRepository ratingEntryRepository)
        {
            this.shoeRepository = shoeRepository;
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
            this.favouritesCollection = favouritesCollection;
            this.services = services;
            this.ratingEntryRepository = ratingEntryRepository;
        }

        
        public IActionResult List(string categoryOrBrand, string sortBy, string query)
        {
            IEnumerable<RatingEntry> ratingEntries = ratingEntryRepository.AllRatings;
            List<RatedShoe> ratedShoes = new List<RatedShoe>();
            int? matchCategoryInDb = shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId;
            int? matchBrandInDb = shoeRepository.AllShoes.FirstOrDefault(s => s.Brand.BrandName == categoryOrBrand)?.ShoeId;
            
            #region //List view from a search query
            //initial List view resulting from a search query
            if (!string.IsNullOrEmpty(query))
            {
                return View(Search(query, ratingEntries, ratedShoes));                
            }

            //after an initial search, then user selects a sortBy option
            if (categoryOrBrand != null && matchCategoryInDb == null && matchBrandInDb == null)
            {
                SortBy sortByEnum = SortByStringToEnum(sortBy);
                ratedShoes = ShowShoesWithSortByOption(ratedShoes, categoryOrBrand, ratingEntries, sortByEnum);

                return View(new ShoesListViewModel
                {
                    CurrentCategoryOrBrand = categoryOrBrand,
                    RatedShoes = ratedShoes
                });

            }
            #endregion


            #region//no search query - just presenting Shoes with category/brand and/or sorting

            ratedShoes = CreateRatedShoeList(shoeRepository.AllShoes, ratingEntries, ratedShoes).ToList();
            bool NoCategoryOrBrandNorAscendingOrDescendingSelected = string.IsNullOrEmpty(categoryOrBrand) && string.IsNullOrEmpty(sortBy);
            bool AscendingOrDescendingButNoCategoryOrBrandSelected = string.IsNullOrEmpty(categoryOrBrand) && !string.IsNullOrEmpty(sortBy);

            
            if (NoCategoryOrBrandNorAscendingOrDescendingSelected) //i.e. just all shoes
            {
                //add all RatedShoe objects to my ratedShoes List 
                ratedShoes = ratedShoes.OrderBy(r => r.Shoe.ShoeId).ToList();

                return View(new ShoesListViewModel
                {
                    CurrentCategoryOrBrand = categoryOrBrand,
                    RatedShoes = ratedShoes
                });
            }

            else if (AscendingOrDescendingButNoCategoryOrBrandSelected)
            {
                SortBy sortByEnum = SortByStringToEnum(sortBy);
                ratedShoes = ShowShoesAscendingOrDescendingWithNoCategoryOrBrand(ratedShoes, sortByEnum);

                return View(new ShoesListViewModel
                {
                    CurrentCategoryOrBrand = categoryOrBrand,
                    RatedShoes = ratedShoes
                });
            }

            else
            {
                ratedShoes = ShowShoesWithCategoryOrBrandSelection(categoryOrBrand, ratedShoes, sortBy, matchCategoryInDb);

                return View(new ShoesListViewModel
                {
                    CurrentCategoryOrBrand = categoryOrBrand,
                    RatedShoes = ratedShoes
                });
            }
            
            
            #endregion
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
            ratedShoes = CreateRatedShoeList(shoes, ratingEntries, ratedShoes).ToList();

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


        private List<RatedShoe> ShowShoesWithCategoryOrBrandSelection(string categoryOrBrand, List<RatedShoe> ratedShoes, string sortBy, int? matchCategoryInDb)
        {
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
            }

            return ratedShoes;
        }



        private ShoesListViewModel Search(string query, IEnumerable<RatingEntry> ratingEntries, List<RatedShoe> ratedShoes)
        {
            IEnumerable<Shoe> shoes = shoeRepository.GetShoesByNameOrBrandOrCategory(query);
            ratedShoes = CreateRatedShoeList(shoes, ratingEntries, ratedShoes).ToList();

            return new ShoesListViewModel
            {
                RatedShoes = ratedShoes,
                CurrentCategoryOrBrand = $"Search results for {query}"
            };
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
                return RedirectToAction("NoFavourites");
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

            return RedirectToAction("ListFavouriteShoes");
        }

        public IActionResult RemoveFromFavourite(int shoeId)
        {
            favouritesCollection.RemoveFromCollection(services, shoeId);

            TempData["favouritedMessage"] = "Shoes were removed from your favourites";

            return RedirectToAction("ListFavouriteShoes");
        }

        public IActionResult AddCommentToFavourite(int favouriteCollectionItemId)
        {
            string comment = Request.Form["Comment"];

            favouritesCollection.SaveComment(services, favouriteCollectionItemId, comment);

            return RedirectToAction("ListFavouriteShoes");
        }


        public IActionResult AddRatingEntry(string shoeId, string rating)
        {
            string referrer = Request.Headers["Referer"].ToString();

            int shoeIdInt = Int32.Parse(shoeId);
            int ratingInt = Int32.Parse(rating);

            bool shoeNotPreviouslyRated = ratingEntryRepository.AddShoeRating(services, shoeIdInt, ratingInt);
            TempData["ratingSubmissionMessage"] = shoeNotPreviouslyRated ? "Thanks for your rating!" : "You can't rate the same shoes twice!";

            return Redirect(referrer);
        }


        public static List<RatedShoe> CreateRatedShoeList(IEnumerable<Shoe> shoes, IEnumerable<RatingEntry> ratingEntries,
            List<RatedShoe> ratedShoes)
        {
            foreach (var shoe in shoes)
            {
                IEnumerable<int> currentShoeRatings = ratingEntries.Where(e => e.ShoeId == shoe.ShoeId).Select(e => e.Rating);
                double overallRating;

                if (currentShoeRatings.Count() != 0)
                {
                    overallRating = currentShoeRatings.Sum() / currentShoeRatings.Count();
                }
                else
                {
                    overallRating = 5; //i.e. new shoes automatically get a 5
                }

                ratedShoes.Add(new RatedShoe
                {
                    OverallRating = overallRating,
                    ShoeId = shoe.ShoeId,
                    Shoe = shoe
                });
            }
            return ratedShoes;
        }

    }
}

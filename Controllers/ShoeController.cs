﻿using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using ClimbingShoebox.Models;
using ClimbingShoebox.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

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

        public IActionResult List(string categoryOrBrand, string ascendingOrDescending)
        {
            IEnumerable<Shoe> shoes;
            IEnumerable<RatingEntry> ratingEntries = ratingEntryRepository.AllRatings;
            List<RatedShoe> ratedShoes = new List<RatedShoe>();

            string currentCategoryOrBrand;


            //No Category or Brand and no ascending/descending selection - i.e. just all shoes
            if (string.IsNullOrEmpty(categoryOrBrand) && string.IsNullOrEmpty(ascendingOrDescending))
            {
                shoes = shoeRepository.AllShoes.OrderBy(s => s.ShoeId);
                currentCategoryOrBrand = "All shoes";

                //add all RatedShoe objects to my ratedShoes List 
                ratedShoes = CreateRatedShoeList(shoes, ratingEntries, ratedShoes);

                return View(new ShoesListViewModel
                {
                    Shoes = shoes,
                    CurrentCategoryOrBrand = currentCategoryOrBrand,
                    RatedShoes = ratedShoes
                });
            }

            //No Category/Brand but ascending/descending selected
            if (categoryOrBrand == "All shoes")
            {
                currentCategoryOrBrand = "All shoes";
                if (ascendingOrDescending == "ascending")
                {
                    shoes = shoeRepository.AllShoes.OrderBy(s => s.Price);
                }
                else
                {
                    shoes = shoeRepository.AllShoes.OrderByDescending(s => s.Price);
                }

                ratedShoes = CreateRatedShoeList(shoes, ratingEntries, ratedShoes);

                return View(new ShoesListViewModel
                {
                    Shoes = shoes,
                    CurrentCategoryOrBrand = currentCategoryOrBrand,
                    RatedShoes = ratedShoes
                });

            }

            //Category/Brand selected but no ascending/descending selected
            if (shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId == null && string.IsNullOrEmpty(ascendingOrDescending))
            {
                shoes = shoeRepository.AllShoes.Where(s => s.Brand.BrandName == categoryOrBrand).OrderBy(s => s.ShoeId);
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


            //Category/Brand selected AND ascending/descending selected
            if (ascendingOrDescending == "ascending")
            {
                if (shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId == null)
                {
                    shoes = shoeRepository.AllShoes.Where(s => s.Brand.BrandName == categoryOrBrand).OrderBy(s => s.Price);
                }
                else
                {
                    shoes = shoeRepository.AllShoes.Where(s => s.Category.CategoryName == categoryOrBrand).OrderBy(s => s.Price);
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
            else
            {
                if (shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId == null)
                {
                    shoes = shoeRepository.AllShoes.Where(s => s.Brand.BrandName == categoryOrBrand).OrderByDescending(s => s.Price);
                }
                else
                {
                    shoes = shoeRepository.AllShoes.Where(s => s.Category.CategoryName == categoryOrBrand).OrderByDescending(s => s.Price);
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

            ratedShoes = CreateRatedShoeList(shoes, ratingEntries, ratedShoes);

            return View(new ShoesListViewModel
            {
                Shoes = shoes,
                CurrentCategoryOrBrand = currentCategoryOrBrand,
                RatedShoes = ratedShoes
            });
        }



        public IActionResult Search(string query)
        {
            IEnumerable<Shoe> shoes;

            if (string.IsNullOrEmpty(query))
            {
                shoes = shoeRepository.AllShoes.OrderBy(s => s.ShoeId);
            }
            else
            {
                shoes = shoeRepository.GetShoesByName(query);
            }

            return View(new ShoesListViewModel
            {
                Shoes = shoes,
                CurrentCategoryOrBrand = $"Search results for {query}"
            });
        }


        public IActionResult Details(int shoeId)
        {
            var shoe = shoeRepository.GetShoebyId(shoeId);
            if (shoe == null)
            {
                return NotFound();
            }
            return View(shoe);
        }


        public IActionResult FavouriteShoes()
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

            return RedirectToAction("FavouriteShoes");
        }

        public IActionResult RemoveFromFavourite(int shoeId)
        {
            favouritesCollection.RemoveFromCollection(services, shoeId);

            TempData["favouritedMessage"] = "Shoes were removed from your favourites";

            return RedirectToAction("FavouriteShoes");
        }

        [HttpPost]
        public IActionResult AddComment(int favouriteCollectionItemId)
        {
            string comment = Request.Form["Comment"];

            favouritesCollection.SaveComment(services, favouriteCollectionItemId, comment);

            return RedirectToAction("FavouriteShoes");
        }


        public IActionResult AddRatingEntry(string shoeId, string rating)
        {
            string referrer = Request.Headers["Referer"].ToString();

            int shoeIdInt = Int32.Parse(shoeId);
            int ratingInt = Int32.Parse(rating);
            
            if(!ratingEntryRepository.AddShoeRating(services, shoeIdInt, ratingInt))
            {
                TempData["alreadyRatedMessage"] = "You can't rate the same shoes twice!";
            }


            return Redirect(referrer);
        }





        public static List<RatedShoe> CreateRatedShoeList(IEnumerable<Shoe> shoes, IEnumerable<RatingEntry> ratingEntries, 
            List<RatedShoe> ratedShoes)
        {
            foreach (var shoe in shoes)
            {
                IEnumerable<int> currentShoeRatings = ratingEntries.Where(e => e.ShoeId == shoe.ShoeId).Select(e => e.Rating);
                double overallRating = currentShoeRatings.Sum() / currentShoeRatings.Count();

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

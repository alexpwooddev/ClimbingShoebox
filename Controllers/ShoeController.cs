using System;
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
using System.Text;

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

        public IActionResult List(string categoryOrBrand, string sortBy, string query)
        {
            IEnumerable<RatingEntry> ratingEntries = ratingEntryRepository.AllRatings;
            List<RatedShoe> ratedShoes = new List<RatedShoe>();
            var matchCategoryInDb = shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId;
            var matchBrandInDb = shoeRepository.AllShoes.FirstOrDefault(s => s.Brand.BrandName == categoryOrBrand)?.ShoeId;
            var shoesInSelectedBrand = ratedShoes.Where(s => s.Shoe.Brand.BrandName == categoryOrBrand);
            var shoesInSelectedCategory = ratedShoes.Where(s => s.Shoe.Category.CategoryName == categoryOrBrand);

            #region //List view from a search query
            //initial List view resulting from a search query
            if (!string.IsNullOrEmpty(query))
            {
                return View(Search(query, ratingEntries, ratedShoes));                
            }

            //after an initial search, then user selects a sortBy option
            if (categoryOrBrand != null && matchCategoryInDb == null && matchBrandInDb == null)
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

                if (sortBy == "ascendingByPrice")
                {
                    ratedShoes = ratedShoes.OrderBy(s => s.Shoe.Price).ToList();
                }
                else if (sortBy == "descendingByPrice")
                {
                    ratedShoes = ratedShoes.OrderByDescending(s => s.Shoe.Price).ToList();
                }
                else if (sortBy == "ascendingByRating")
                {
                    ratedShoes = ratedShoes.OrderBy(s => s.OverallRating).ToList();
                }
                else
                {
                    ratedShoes = ratedShoes.OrderByDescending(s => s.OverallRating).ToList();
                }

                return View(new ShoesListViewModel
                {
                    CurrentCategoryOrBrand = categoryOrBrand,
                    RatedShoes = ratedShoes
                });

            }
            #endregion

            #region//no search query - just presenting Shoes with category/brand and/or sorting
            ratedShoes = CreateRatedShoeList(shoeRepository.AllShoes, ratingEntries, ratedShoes).ToList();

            //No Category or Brand and no ascending/descending selection - i.e. just all shoes
            if (string.IsNullOrEmpty(categoryOrBrand) && string.IsNullOrEmpty(sortBy))
            {
                //add all RatedShoe objects to my ratedShoes List 
                ratedShoes = ratedShoes.OrderBy(r => r.Shoe.ShoeId).ToList();

                return View(new ShoesListViewModel
                {
                    CurrentCategoryOrBrand = categoryOrBrand,
                    RatedShoes = ratedShoes
                });
            }

            //No Category/Brand but ascending/descending selected
            else if (string.IsNullOrEmpty(categoryOrBrand) && !string.IsNullOrEmpty(sortBy))
            {
                if (sortBy == "ascendingByPrice")
                {
                    ratedShoes = ratedShoes.OrderBy(s => s.Shoe.Price).ToList();
                }
                else if (sortBy == "descendingByPrice")
                {
                    ratedShoes = ratedShoes.OrderByDescending(s => s.Shoe.Price).ToList();
                }
                else if (sortBy == "ascendingByRating")
                {
                    ratedShoes = ratedShoes.OrderBy(s => s.OverallRating).ToList();
                }
                else
                {
                    ratedShoes = ratedShoes.OrderByDescending(s => s.OverallRating).ToList();
                }

                return View(new ShoesListViewModel
                {
                    CurrentCategoryOrBrand = categoryOrBrand,
                    RatedShoes = ratedShoes
                });

            }

            //Category/Brand selected but no sort selected
            else if (categoryOrBrand != "All shoes" && string.IsNullOrEmpty(sortBy))
            {
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
                if (sortBy == "ascendingByPrice")
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

                else if (sortBy == "descendingByPrice")
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

                else if (sortBy == "ascendingByRating")
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

            return View(new ShoesListViewModel
            {
                CurrentCategoryOrBrand = categoryOrBrand,
                RatedShoes = ratedShoes
            });
            #endregion
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

            if (!ratingEntryRepository.AddShoeRating(services, shoeIdInt, ratingInt))
            {
                TempData["alreadyRatedMessage"] = "You can't rate the same shoes twice!";
            }
            else
            {
                TempData["ratingSubmittedMessage"] = "Thanks for your rating!";
            }


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

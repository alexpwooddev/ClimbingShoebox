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

        public IActionResult List(string categoryOrBrand, string sortBy)
        {
            IEnumerable<RatingEntry> ratingEntries = ratingEntryRepository.AllRatings;
            List<RatedShoe> ratedShoes = new List<RatedShoe>();
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
            else if (categoryOrBrand == "All shoes")
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
                if (shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId == null)
                {
                    ratedShoes = ratedShoes.Where(s => s.Shoe.Brand.BrandName == categoryOrBrand).OrderBy(s => s.Shoe.ShoeId).ToList();
                }
                else
                {
                    ratedShoes = ratedShoes.Where(s => s.Shoe.Category.CategoryName == categoryOrBrand).OrderBy(s => s.Shoe.ShoeId).ToList();
                }
            }
            
            //category/Brand selected AND sort selected
            else 
            {
                if (sortBy == "ascendingByPrice")
                {
                    if (shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId == null)
                    {
                        ratedShoes = ratedShoes.Where(s => s.Shoe.Brand.BrandName == categoryOrBrand).OrderBy(s => s.Shoe.Price).ToList();
                    }
                    else
                    {
                        ratedShoes = ratedShoes.Where(s => s.Shoe.Category.CategoryName == categoryOrBrand).OrderBy(s => s.Shoe.Price).ToList();
                    }
                }

                else if (sortBy == "descendingByPrice")
                {
                    if (shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId == null)
                    {
                        ratedShoes = ratedShoes.Where(s => s.Shoe.Brand.BrandName == categoryOrBrand).OrderByDescending(s => s.Shoe.Price).ToList();
                    }
                    else
                    {
                        ratedShoes = ratedShoes.Where(s => s.Shoe.Category.CategoryName == categoryOrBrand).OrderByDescending(s => s.Shoe.Price).ToList();
                    }
                }

                else if (sortBy == "ascendingByRating")
                {
                    if (shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId == null)
                    {
                        ratedShoes = ratedShoes.Where(s => s.Shoe.Brand.BrandName == categoryOrBrand).OrderBy(s => s.OverallRating).ToList();
                    }
                    else
                    {
                        ratedShoes = ratedShoes.Where(s => s.Shoe.Category.CategoryName == categoryOrBrand).OrderBy(s => s.OverallRating).ToList();
                    }
                }

                else //sortBy == descendingByRating
                {
                    if (shoeRepository.AllShoes.FirstOrDefault(s => s.Category.CategoryName == categoryOrBrand)?.ShoeId == null)
                    {
                        ratedShoes = ratedShoes.Where(s => s.Shoe.Brand.BrandName == categoryOrBrand).OrderByDescending(s => s.OverallRating).ToList();
                    }
                    else
                    {
                        ratedShoes = ratedShoes.Where(s => s.Shoe.Category.CategoryName == categoryOrBrand).OrderByDescending(s => s.OverallRating).ToList();
                    }
                }
            }           

            return View(new ShoesListViewModel
            {
                CurrentCategoryOrBrand = categoryOrBrand,
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

            IEnumerable<RatingEntry> ratingEntries = ratingEntryRepository.AllRatings;
            List<RatedShoe> ratedShoes = new List<RatedShoe>();
            ratedShoes = CreateRatedShoeList(shoes, ratingEntries, ratedShoes).ToList();

            return View(new ShoesListViewModel
            {
                RatedShoes = ratedShoes,
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

using ClimbingShoebox.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingShoebox.Controllers
{
    public class ShoeRatingController : Controller
    {
        private readonly IRatingEntryRepository ratingEntryRepository;
        private readonly IServiceProvider services;

        public ShoeRatingController(IRatingEntryRepository ratingEntryRepository, IServiceProvider services)
        {
            this.ratingEntryRepository = ratingEntryRepository;
            this.services = services;
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

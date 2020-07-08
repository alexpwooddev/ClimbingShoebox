using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class RatingEntryRepository : IRatingEntryRepository
    {
        private readonly AppDbContext appDbContext;


        public RatingEntryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<RatingEntry> AllRatings
        {
            get
            {
                return appDbContext.RatingEntries.Include(e => e.Shoe);
            }
        }


        public IEnumerable<int> GetRatings(int shoeId)
        {
            var ratingsList = appDbContext.RatingEntries.Where(e => e.ShoeId == shoeId).Select(e => e.Rating);
            
            /*
            var ratingsList = from e in appDbContext.RatingEntries
                              where e.ShoeId == shoeId
                              select e.Rating;
            */
            return ratingsList;
        }

        public double OverallRatingForShoe(int shoeId)
        {
            var ratingsList = GetRatings(shoeId);

            return ratingsList.Sum() / ratingsList.Count();

        }


        public bool AddShoeRating(IServiceProvider services, int shoeId, int rating)
        {
            var userId = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (appDbContext.RatingEntries.Where(e => e.ApplicationUserId == userId && e.ShoeId == shoeId).Count() == 0)
            {
                var ratingEntry = new RatingEntry
                {
                    Rating = rating,
                    ShoeId = shoeId,
                    ApplicationUserId = userId
                };

                appDbContext.RatingEntries.Add(ratingEntry);
                appDbContext.SaveChanges();
                
                //this flags that a rating was added 
                return true;
            }
            //a rating wasn't added
            return false;
            
        }

        public IEnumerable<int> GetShoeRatings(int shoeId)
        {
            throw new NotImplementedException();
        }


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class FavouriteShoeRepository : IFavouriteShoeRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public FavouriteShoeRepository(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.appDbContext = appDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

       



        public IEnumerable<FavouriteShoe> AllFavouriteShoes
        {
            get
            {
                return appDbContext.FavouriteShoes;
            }
        }

        public FavouriteShoe CurrentFavouriteShoe
        {

            get
            {
                var currentUser = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                

                return appDbContext.FavouriteShoes.FirstOrDefault(f => f.ApplicationUserId == currentUser);
            }
        }

        public void AddFavourite(int shoeId)
        {
            //clear current favourite first
            appDbContext.Database.ExecuteSqlCommand("delete from FavouriteShoes");
            

            var shoe = appDbContext.Shoes.FirstOrDefault(s => s.ShoeId == shoeId);

            var favouriteShoe = new FavouriteShoe
            {
                ShoeId = shoeId,
                ApplicationUserId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
            };

            appDbContext.FavouriteShoes.Add(favouriteShoe);

            appDbContext.SaveChanges();
        }

        public void RemoveFavourite(int shoeId)
        {
            var favouriteShoe = appDbContext.FavouriteShoes.FirstOrDefault(f => f.ShoeId == shoeId);

            appDbContext.FavouriteShoes.Remove(favouriteShoe);
            appDbContext.SaveChanges();
        }
    }
}

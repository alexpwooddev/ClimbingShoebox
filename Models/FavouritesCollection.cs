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
    public class FavouritesCollection
    {
        private readonly AppDbContext appDbContext;

        public string FavouritesCollectionId { get; set; }

        public List<FavouritesCollectionItem> FavouritesCollectionItems { get; set; }



        private FavouritesCollection(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public static FavouritesCollection GetCollection(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            string collectionId = session.GetString("CollectionId") ?? Guid.NewGuid().ToString();

            session.SetString("CollectionId", collectionId);

            return new FavouritesCollection(context) { FavouritesCollectionId = collectionId };

        }

        public void AddToCollection(IServiceProvider services, int shoeId)
        {
            var userId = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var shoe = appDbContext.Shoes.FirstOrDefault(s => s.ShoeId == shoeId);

            var favouritesCollectionItem = appDbContext.FavouritesCollectionItems.SingleOrDefault
                (c => c.Shoe.ShoeId == shoeId && c.FavouritesCollectionId == FavouritesCollectionId);

            //check if already in collection
            //if not in collection, add it
            if (favouritesCollectionItem == null)
            {
                favouritesCollectionItem = new FavouritesCollectionItem
                {
                    FavouritesCollectionId = FavouritesCollectionId,
                    Shoe = shoe,
                    ShoeId = shoe.ShoeId,
                    ApplicationUserId = userId
                };

                appDbContext.FavouritesCollectionItems.Add(favouritesCollectionItem);
            }

            appDbContext.SaveChanges();
        }


        public void RemoveFromCollection(int shoeId)
        {
            var favouritesCollectionItem = appDbContext.FavouritesCollectionItems.SingleOrDefault
                (c => c.Shoe.ShoeId == shoeId && c.FavouritesCollectionId == FavouritesCollectionId);

            //if it exists, then remove it
            if (favouritesCollectionItem != null)
            {
                appDbContext.FavouritesCollectionItems.Remove(favouritesCollectionItem);
            }

          appDbContext.SaveChanges();

        }


        public List<FavouritesCollectionItem> GetCollectionItems()
        {
            return FavouritesCollectionItems ??
                (FavouritesCollectionItems = appDbContext.FavouritesCollectionItems.
                Where(c => c.FavouritesCollectionId == FavouritesCollectionId)
                .Include(s => s.Shoe)
                .ToList());
        }
    }
}

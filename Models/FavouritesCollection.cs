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

        //public string FavouritesCollectionId { get; set; }

        public List<FavouritesCollectionItem> FavouritesCollectionItems { get; set; }



        private FavouritesCollection(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public static FavouritesCollection GetCollection(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            //string collectionId = session.GetString("CollectionId") ?? Guid.NewGuid().ToString();

            //session.SetString("CollectionId", collectionId);

            return new FavouritesCollection(context);
            
            //{ FavouritesCollectionId = collectionId };

        }

        public void AddToCollection(IServiceProvider services, int shoeId)
        {
            var userId = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shoe = appDbContext.Shoes.FirstOrDefault(s => s.ShoeId == shoeId);
            var favouritesCollectionItem = appDbContext.FavouritesCollectionItems.SingleOrDefault
                (c => c.Shoe.ShoeId == shoeId && c.ApplicationUserId == userId);

            //check if already in collection
            //if not in collection, make a new item then add it to the database of items
            if (favouritesCollectionItem == null)
            {
                favouritesCollectionItem = new FavouritesCollectionItem
                {
                    Shoe = shoe,
                    ShoeId = shoe.ShoeId,
                    ApplicationUserId = userId,
                    Comment = "Add your comments here"
                };

                appDbContext.FavouritesCollectionItems.Add(favouritesCollectionItem);
            }

            appDbContext.SaveChanges();
        }


        public void RemoveFromCollection(IServiceProvider services, int shoeId)
        {
            var userId = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favouritesCollectionItem = appDbContext.FavouritesCollectionItems.SingleOrDefault
                (c => c.Shoe.ShoeId == shoeId && c.ApplicationUserId == userId);

            //if it exists, then remove it
            if (favouritesCollectionItem != null)
            {
                appDbContext.FavouritesCollectionItems.Remove(favouritesCollectionItem);
            }

          appDbContext.SaveChanges();

        }


        public List<FavouritesCollectionItem> GetCollectionItems(IServiceProvider services)
        {
            var userId = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return FavouritesCollectionItems ??
                (FavouritesCollectionItems = appDbContext.FavouritesCollectionItems.
                Where(c => c.ApplicationUserId == userId)
                .Include(s => s.Shoe)
                .ToList());
        }


        public void SaveComment(IServiceProvider services, int favouritesCollectionItemId, string comment)
        {
            var userId = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favouritesCollectionItem = appDbContext.FavouritesCollectionItems.SingleOrDefault
                (c => c.FavouritesCollectionItemId == favouritesCollectionItemId);

            favouritesCollectionItem.Comment = comment;

            appDbContext.FavouritesCollectionItems.Update(favouritesCollectionItem);
            appDbContext.SaveChanges();
            
        }
    }
}

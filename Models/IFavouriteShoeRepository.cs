using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public interface IFavouriteShoeRepository
    {
        IEnumerable<FavouriteShoe> AllFavouriteShoes { get; }
        FavouriteShoe CurrentFavouriteShoe { get; }

        void AddFavourite(int shoeId);

        void RemoveFavourite(int shoeId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public interface IShoeRepository
    {
        IEnumerable<Shoe> AllShoes { get; }
        Shoe FeaturedShoe { get; }
        Shoe GetShoebyId(int shoeId);
        IEnumerable<Shoe> GetShoesByName(string nameQuery);
    }
}

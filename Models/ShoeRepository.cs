using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class ShoeRepository : IShoeRepository
    {
        private readonly AppDbContext appDbContext;

        public ShoeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        
        public IEnumerable<Shoe> AllShoes
        {
            get
            {
                return appDbContext.Shoes.Include(c => c.Category).Include(b => b.Brand);
            }
        }

        public Shoe FeaturedShoe
        {
            get
            {
                return appDbContext.Shoes.FirstOrDefault(s => s.IsFeaturedShoe == true);
            }
        }

        public Shoe GetShoebyId(int shoeId)
        {
            return appDbContext.Shoes.FirstOrDefault(s => s.ShoeId == shoeId);
        }

        public IEnumerable<Shoe> GetShoesByNameOrBrandOrCategory(string nameQuery) => appDbContext.Shoes.Where(s => s.Name.Contains(nameQuery) || s.Brand.BrandName.Contains(nameQuery) ||
            s.Category.CategoryName.Contains(nameQuery) || string.IsNullOrEmpty(nameQuery));
        
    }
}

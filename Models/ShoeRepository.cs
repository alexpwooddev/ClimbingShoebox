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
                return appDbContext.Shoes.Include(c => c.Category);
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
            
        }
    }
}

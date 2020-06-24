using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext appDbContext;

        public BrandRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<Brand> AllBrands => appDbContext.Brands;
    }
}

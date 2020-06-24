using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class MockBrandRepository : IBrandRepository
    {
        public IEnumerable<Brand> AllBrands =>
            new List<Brand>
            {
                new Brand { BrandId = 1, BrandName = "La Sportiva" },
                new Brand { BrandId = 2, BrandName = "Scarpa" },
                new Brand { BrandId = 3, BrandName = "Five-Ten" }
            };

    }
}

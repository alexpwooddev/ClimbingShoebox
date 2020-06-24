using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> AllCategories =>
            new List<Category>
            {
                new Category{ CategoryId = 1, CategoryName = "Multi-Pitch" },
                new Category{ CategoryId = 2, CategoryName = "Sport Climbing" },
                new Category{ CategoryId = 3, CategoryName = "Bouldering" }
            };
    }
}

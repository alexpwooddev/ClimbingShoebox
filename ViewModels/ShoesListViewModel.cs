using ClimbingShoebox.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.ViewModels
{
    public class ShoesListViewModel
    {
        public IEnumerable<Shoe> Shoes { get; set; }

        public string CurrentCategoryOrBrand { get; set; }

        public List<RatedShoe> RatedShoes { get; set; }

        [TempData]
        public string tempMessage { get; set; }

    }
}

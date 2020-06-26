using ClimbingShoebox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.ViewModels
{
    public class HomeViewModel
    {
        public Shoe FeaturedShoe { get; set; }

        public string query { get; set; }
    }
}

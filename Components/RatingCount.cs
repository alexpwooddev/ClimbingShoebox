using ClimbingShoebox.Models;
using ClimbingShoebox.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Components
{
    public class RatingCount : ViewComponent
    {
        private readonly IRatingEntryRepository ratingEntryRepository;

        public RatingCount(IRatingEntryRepository ratingEntryRepository)
        {
            this.ratingEntryRepository = ratingEntryRepository;
        }

        public IViewComponentResult Invoke(int shoeId)
        {
            var ratingCountViewModel = new RatingCountViewModel
            {
                RatingCount = ratingEntryRepository.AllRatings.Where(r => r.ShoeId == shoeId).Count()
            };

            return View(ratingCountViewModel);
        }
    }
}

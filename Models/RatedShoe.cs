using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class RatedShoe
    {
        public int RatedShoeId { get; set; }
        [ForeignKey("ShoeId")]
        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; }
        public double OverallRating { get; set; }

    }
}

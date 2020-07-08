using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class RatingEntry
    {
        public int RatingEntryId { get; set; }
        
        [Range(0, 5)]
        public int Rating { get; set; }

        public Shoe Shoe { get; set; }
        [ForeignKey("ShoeId")]
        public int ShoeId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}

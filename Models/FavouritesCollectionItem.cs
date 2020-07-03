using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class FavouritesCollectionItem
    {
        
        public int FavouritesCollectionItemId { get; set; }
        public Shoe Shoe { get; set; }
        
        [ForeignKey("ShoeId")]
        public int ShoeId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    } 
}

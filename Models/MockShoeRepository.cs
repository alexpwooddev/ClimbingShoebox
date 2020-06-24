using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class MockShoeRepository : IShoeRepository
    {

        private readonly ICategoryRepository categoryRepository = new MockCategoryRepository();
        
        public IEnumerable<Shoe> AllShoes =>
            new List<Shoe>
            {
                new Shoe {ShoeId = 1,
                Name = "La Sportiva Futura",
                Price = 101.25M,
                ShortDescription = "Top of the range climbing shoe from Sportiva with No-Edge construction to allow your foot to come into closer contact with the rock for greater sensitivity.",
                LongDescription = "The La Sportiva Futura is an aggressive high end climbing shoe which features the No-Edge construction. No-Edge means that the the traditional edge of the sole is eliminated in favour of a rounded profile, this allows your foot to get closer to the rock than ever before increasing sensitivity and also distributing the load better for a more homogeneous push. The Futura is built on a fairly downturned last which means that they will perform well on overhanging routes with the extra pull which is generated, but they are not so severe that vertical/slab climbing is made difficult. Other details of the Futura include the P3 permanent power platform to maintain the tension and shape of the shoe throughout their life and a fast lacing system which tightens accurately but also lets you get your boots on and off sharpish.",
                ImageUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_gallery_full/public/product-images/footwear/la_sportiva_futura.jpg?itok=mk3tuk6k",
                ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/la_sportiva_futura.jpg?itok=uFdgfcwB",
                InStock = true,
                IsFeaturedShoe = false,
                CategoryId = 3,
                BrandId = 1},
                new Shoe {ShoeId = 2,
                Name = "La Sportiva Solution",
                Price = 90.73M,
                ShortDescription = "This is the top model from Sportiva. Its superb foot-hugging design has been developed specifically for bouldering.",
                LongDescription = "The La Sportiva Solution is a revolutionary rock shoe developed specifically for bouldering. Whatever technical problem the rock presents, the “Solution” rock shoe gives you the support you need with the most innovative technology to overcome the boulder move. The upper uses Lock Harness System® technology, which hugs the foot from the inside and combines with the unique deep heel-cups to ensure maximum flexibility when hooking, on incuts and overhangs. The La Sportiva Solution utilises the P3® (Permanent Power Platform) randing system, an active part of the shoe which gives it its versatility and works in synergy with the base of the foot to spread and maintain tension through time. The innovative Fast Lacing System® combined with the tongue in elasticized fabric",
                ImageUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_gallery_full/public/product-images/footwear/la_sportiva_solution.jpg?itok=G2m7MQEF",
                ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/la_sportiva_solution.jpg?itok=Tx5pnWJp",
                InStock = true,
                IsFeaturedShoe = false,
                CategoryId = 3,
                BrandId = 1},
                new Shoe {ShoeId = 3,
                Name = "Scarpa Maestro",
                Price = 82.50M,
                ShortDescription = "An entirely original shoe primarily designed for performance big wall and multi-pitch climbing but will equally do well on sport, trad, and boulder routes, depending on your style and fit.",
                LongDescription = "The Scarpa Maestro is the first shoe in Scarpa's new Maestro family, with stiffness, comfort, and performance all primarily designed for hard big wall and multi-pitch climbing. Scarpa have built this shoe from the ground up, creating an entirely new last, stitch pattern, plastic midsole, and active randing system suitable for a technical shoe that can be worn all day. This is Scarpa's most technical performance shoe designed for stiffness and all day comfort. This brand new shoe features a completely original construction. The full length 1.1mm/1.4mm Talyn midsole provides a high level of stiffness to support the foot for longer climbs, whilst edging, and standing on small crystals, but, to cater for slabs and more featureless climbing, Scarpa have punched three holes in the front section of the midsole to allow the toe area to flex and smear. The IPC-Tensions active randing system is similar to the Bi-Tension system, but it adds a third strip under the sole of the shoe to provide additional strength on edges and micro footholds. This extra rand will also maintain the shoe's performance shape for longer. And, the Vibram XS Edge rubber is actually only half the story, as the heel utitilises the higher friction Grip 2 compound. Not a bad idea as any heel hooks you come across will benefit from this grippier compound.",          
                ImageUrl = "https://cdn.mec.ca/medias/sys_master/high-res/high-res/8938729078814/5053104-STN13.jpg",
                ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/untitled-1_0.jpg?itok=kcONkP4K",
                InStock = true,
                IsFeaturedShoe = false,
                CategoryId = 1,
                BrandId = 2}
            };



        public Shoe FeaturedShoe { get; }

        public Shoe GetShoebyId(int shoeId)
        {
            return AllShoes.FirstOrDefault(s => s.ShoeId == shoeId);
        }
    }
}

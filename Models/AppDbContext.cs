using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<FavouriteShoe> FavouriteShoes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed categories
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "Multi-Pitch" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Sport Climbing" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Bouldering" });

            //seed brands
            modelBuilder.Entity<Brand>().HasData(new Brand { BrandId = 1, BrandName = "La Sportiva" });
            modelBuilder.Entity<Brand>().HasData(new Brand { BrandId = 2, BrandName = "Scarpa" });
            modelBuilder.Entity<Brand>().HasData(new Brand { BrandId = 3, BrandName = "Five-Ten" });


            modelBuilder.Entity<Shoe>().HasData(new Shoe
            {
                ShoeId = 1,
                Name = "La Sportiva Futura",
                Price = 101.25M,
                ShortDescription = "Top of the range climbing shoe from Sportiva with No-Edge construction to allow your foot to come into closer contact with the rock for greater sensitivity.",
                LongDescription = "The La Sportiva Futura is an aggressive high end climbing shoe which features the No-Edge construction. No-Edge means that the the traditional edge of the sole is eliminated in favour of a rounded profile, this allows your foot to get closer to the rock than ever before increasing sensitivity and also distributing the load better for a more homogeneous push. The Futura is built on a fairly downturned last which means that they will perform well on overhanging routes with the extra pull which is generated, but they are not so severe that vertical/slab climbing is made difficult. Other details of the Futura include the P3 permanent power platform to maintain the tension and shape of the shoe throughout their life and a fast lacing system which tightens accurately but also lets you get your boots on and off sharpish.",
                ImageUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_gallery_full/public/product-images/footwear/la_sportiva_futura.jpg?itok=mk3tuk6k",
                ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/la_sportiva_futura.jpg?itok=uFdgfcwB",
                InStock = true,
                IsFeaturedShoe = false,
                CategoryId = 3,
                BrandId = 1

            });

            modelBuilder.Entity<Shoe>().HasData(new Shoe
            {
                ShoeId = 2,
                Name = "La Sportiva Solution",
                Price = 90.73M,
                ShortDescription = "This is the top model from Sportiva. Its superb foot-hugging design has been developed specifically for bouldering.",
                LongDescription = "The La Sportiva Solution is a revolutionary rock shoe developed specifically for bouldering. Whatever technical problem the rock presents, the “Solution” rock shoe gives you the support you need with the most innovative technology to overcome the boulder move. The upper uses Lock Harness System® technology, which hugs the foot from the inside and combines with the unique deep heel-cups to ensure maximum flexibility when hooking, on incuts and overhangs. The La Sportiva Solution utilises the P3® (Permanent Power Platform) randing system, an active part of the shoe which gives it its versatility and works in synergy with the base of the foot to spread and maintain tension through time. The innovative Fast Lacing System® combined with the tongue in elasticized fabric",
                ImageUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_gallery_full/public/product-images/footwear/la_sportiva_solution.jpg?itok=G2m7MQEF",
                ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/la_sportiva_solution.jpg?itok=Tx5pnWJp",
                InStock = true,
                IsFeaturedShoe = false,
                CategoryId = 3,
                BrandId = 1

            });

            modelBuilder.Entity<Shoe>().HasData(new Shoe
            {
                ShoeId = 3,
                Name = "Scarpa Maestro",
                Price = 82.50M,
                ShortDescription = "An entirely original shoe primarily designed for performance big wall and multi-pitch climbing but will equally do well on sport, trad, and boulder routes, depending on your style and fit.",
                LongDescription = "The Scarpa Maestro is the first shoe in Scarpa's new Maestro family, with stiffness, comfort, and performance all primarily designed for hard big wall and multi-pitch climbing. Scarpa have built this shoe from the ground up, creating an entirely new last, stitch pattern, plastic midsole, and active randing system suitable for a technical shoe that can be worn all day. This is Scarpa's most technical performance shoe designed for stiffness and all day comfort.This brand new shoe features a completely original construction.The full length 1.1mm / 1.4mm Talyn midsole provides a high level of stiffness to support the foot for longer climbs, whilst edging, and standing on small crystals, but, to cater for slabs and more featureless climbing, Scarpa have punched three holes in the front section of the midsole to allow the toe area to flex and smear.The IPC - Tensions active randing system is similar to the Bi - Tension system, but it adds a third strip under the sole of the shoe to provide additional strength on edges and micro footholds.This extra rand will also maintain the shoe's performance shape for longer. And, the Vibram XS Edge rubber is actually only half the story, as the heel utitilises the higher friction Grip 2 compound. Not a bad idea as any heel hooks you come across will benefit from this grippier compound.",
                ImageUrl = "https://cdn.mec.ca/medias/sys_master/high-res/high-res/8938729078814/5053104-STN13.jpg",
                ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/untitled-1_0.jpg?itok=kcONkP4K",
                InStock = true,
                IsFeaturedShoe = false,
                CategoryId = 1,
                BrandId = 2


            });

            modelBuilder.Entity<Shoe>().HasData(new Shoe
            {
                ShoeId = 4,
                Name = "Five-Ten Anasazi Blanco",
                Price = 95.83M,
                ShortDescription = "The famously stiff anasazi which has been revised with a thermoplastic midsole to keep its rigidity for longer. Combining this with a high tension heel rand produces one of the most powerful edging shoes available.",
                LongDescription = "The Five Ten Anasazi Blanco has been revised with a thermoplastic midsole so that it keeps its famous stiffness for longer. A combination of the stiff midsole, high tension heel rand and C4 rubber outsole provides one of the most powerful edging shoes on the market. The lacing system is long to give a precise fit and has been updated to a welded construction for a slicker look.",
                ImageUrl = "https://shop.cdn.epictv.com/0cnbjeP2TUq6MogetRAp-44923e8eb357c847d2217f5c1ff0736d.jpeg",
                ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/osclegacy/five_ten_anasazi_blanco_1.jpg?itok=vDcBJG_c",
                InStock = true,
                IsFeaturedShoe = false,
                CategoryId = 1,
                BrandId = 3

            });

            modelBuilder.Entity<Shoe>().HasData(new Shoe
            {
                ShoeId = 5,
                Name = "Scarpa Instinct Lace",
                Price = 87.50M,
                ShortDescription = "Incredibly comfortably for an aggressive high performance shoe the Instinct Lace will perform brilliantly for sport, bouldering and even high end trad routes. The laces allow for a really precise fit and Vibram XS Edge rubber means your accurate foot placements stay where they are.",
                LongDescription = "The Scarpa Instinct Lace is a great all round performer for sport climbing, bouldering and hard trad routes. The lace system allows for a really accurate fit so your foot placements will be even more precise and power is transfered better through the shoe.This latest evolution of the Instinct lace is far lighter and more sensitive than the old version.The new design features a microfibre upper and 3/4 outsole along with a TPU cage which acts to support the foot and reduce torsional movement and roll during edging.It has also been updated with the same heel cup as the VS, which has proven to be a far better fit for the majority of people.",
                ImageUrl = "https://shop.epictv.com/sites/default/files/a09a6600c69aac789852759037039727.jpeg",
                ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/osclegacy/Scarpa_Instinct_Lace.jpg?itok=e05Rnm1Q",
                InStock = true,
                IsFeaturedShoe = false,
                CategoryId = 2,
                BrandId = 2


            });

            modelBuilder.Entity<Shoe>().HasData(new Shoe
            {
                ShoeId = 6,
                Name = "Five-Ten Hiangle",
                Price = 74.17M,
                ShortDescription = "Stiff, comfortable downturned shoe that is great for tiny edges and steep overhanging routes.",
                LongDescription = "The Hiangle, one of Five Ten’s most popular climbing shoes, has received an update. Built on the same last as the blue Hiangles it now has an unlined microfibre upper for shape retention and stretch rebound, a redesigned heel and increased heel tensioning. The heel is now sensitive Stealth® HF rubber that hugs smears with has increased rand tension to keep heel contact locked down. The new Hiangle is also now made with a split outsole making it more flexible for better performance in competition style indoor climbing and on volumes. The Hiangle keeps it's downturned shape and extended toe rubber for secure toe hooks.",
                ImageUrl = "https://www.bfgcdn.com/1500_1500_90/301-0637-0111/five-ten-hiangle-climbing-shoes.jpg",
                ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/hiangle_white_side-lateral.jpg?itok=lRP5wBht",
                InStock = true,
                IsFeaturedShoe = false,
                CategoryId = 2,
                BrandId = 3

            });


            modelBuilder.Entity<Shoe>().HasData(new Shoe
            {
                ShoeId = 7,
                Name = "Scarpa Instinct VS-R",
                Price = 91.25M,
                ShortDescription = "A softer version of the highly popular Instinct VS with Vibram XS Grip rubber rather than XS Edge for a greater all round performance (rather than being tailored towards edging like the VS.",
                LongDescription = "The Scarpa Instinct VS-R is essentially a softer version of the incredibly popular Instinct VS, it uses the same last and rand as the VS for high levels of sensitivity and accurate foot placements. The VS-R has a semi-aggressive profile will work well at any angle but will still allow you to pull in on holds when on overhanging terrain.The use of a softer mid-sole and Vibram XS Grip rubber makes for a more all round round performance rather than being a bit more biased towards edging like the Instinct VS and also makes them more sensitive.",
                ImageUrl = "https://shop.cdn.epictv.com/FV7D0lhnRWWhADXL2OLw-917c326a874c1f255002a0fb7e4ec25d.jpeg",
                ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/osclegacy/Scarpa_Instinct_VSR.jpg?itok=xOczMULQ",
                InStock = true,
                IsFeaturedShoe = true,
                CategoryId = 3,
                BrandId = 2

            });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ClimbingShoebox.Migrations
{
    public partial class MoreShoeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shoes",
                columns: new[] { "ShoeId", "BrandId", "CategoryId", "ImageThumbnailUrl", "ImageUrl", "InStock", "IsFeaturedShoe", "LongDescription", "Name", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { 8, 1, 2, "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/osclegacy/La_Sportiva_Genius.jpg?itok=59X-cbZm", "https://img02.aws.kooomo-cloud.com/upload/la-sportiva/images/10R_123_big.jpg?v=19", true, false, "The La Sportiva Genius, ridiculous name, great shoe. These are basically a lace up version of the futura but the addition of laces makes these a great choice for those looking for a more precise fit or have a lower volume foot. No Edge Technology ensures your toes are always as close to the rock as possible and when combined with the 3mm Vibram XS Grip 2 rubber they offer incredible sensitivity. The P3 midsole ensures power is always pushed toward the toes and the downturned profile of the shoe is not lost over time.", "La Sportiva Genius", 97.50m, "Essentially these are a lace up Futura, they feature No Edge technology, P3 midsole, and 3mm Vibram XS Grip 2 rubber on the sole for excellent sensitivity. The laces give you an even more precise fit and these actually seem surprisingly comfy for the level of performance given." },
                    { 9, 2, 3, "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/untitled-1_5.jpg?itok=FSqGbvvz", "https://shop.cdn.epictv.com/XiewyPwRQoiypQKFfl6i-fc8c8814d1d66641612cae26a8a545a5.jpeg", true, false, "The Scarpa Furia S is built on the same last as the original Furia, but a few updates have been made to give it the 'S' (Soft) tag. Chiefly amongst these is the new Wave Closure System, which replaces the double velcro straps witha a more streamlined single strap closure system. A new rand called the IPR-Tension also reduces excess material, without decreasing performance, to increase the sensitivity and overall flexibility of the shoe.", "Scarpa Furia S", 84.38m, "The Scarpa Furia S is an aggressive climbing shoe with an extremely sensitive design. The FZ last is highly asymmetric, downturned, and with a medium-to-low angled toe-box that will suit overhanging problems. The sole is Vibram XS Grip2 3.5 mm and the upper is microfibre to limit the shoe's stretch completely." },
                    { 10, 2, 3, "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/scarpa_arpia_womens.jpg?itok=phWKH7YY", "https://shop.cdn.epictv.com/products/skis/scarpa_arpia_womens.jpg", true, false, "The Scarpa Arpia Womens is an excellent choice for those who want to progress to a high performace shoe without having to deal with the a shape which is too aggressive. The womens specific last has a lower volume than the mens version and features a slightly downturned profile and moderate asymmetry, this means that the Arpia has high comfort levels whilst providing a step up in terms of performance. It is a mid-stiffness shoe that gives enough support to your toes and maintains smearing ability and sensitivity. A zig zag style closure dubbed the Wave-Closure-System makes for great adjustability and quick on-offs. The Arpia Womens is not limited to any one discipline of climbing and should excel in any situation from multi-pitch trad routes to indoor bouldering.", "Scarpa Arpia Women's", 64.17m, "An excellent gateway to a high end shoe. With a slight downturn and moderate asymmetry they perform to a high level whilst maintaining comfort." },
                    { 11, 3, 2, "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/bc0861_sl_b2ccat.jpg?itok=-7c3wc95", "https://shop.cdn.epictv.com/KNrphkguRyur7cqWNNOW-ab73f542b6d60c4de151800b8abc0a6c.jpeg", true, false, "The Five Ten Aleon is a high tech bouldering and sport climbing shoe designed by Fred Nicole. It is a medium stiffness shoe which is asymmetric and moderately downturned, This stiffer midsole has a diamond cut out in the toe box to retain flex and senitivity whilst giving more support on tiny holds. A brand new last has a wider forefoot and a more pointed toe box than others in the Five Ten range, this directs power to the big toe and offers greater precision. The Aleon also features a very strong tension rand and a high arch which further reinforces the power being put through to the big toe.", "Five-Ten Aleon", 84.17m, "High end, medium stiffness shoes designed by Fred Nicole.....enough said." },
                    { 12, 3, 3, "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/five_ten_anasazi_pro_0.jpg?itok=hRWRDNAQ", "https://shop.cdn.epictv.com/m0nFfiMuRuuPq4iUIb4F-9d684c589d67031a627ad33d59db65e5.jpeg", true, false, "The Five Ten Anasazi Pro takes the basic Anasazi VCS model and adds some uprated details. First there is a Stealth Mi6 toe patch for extra toe hooking ability indoors and out, the Mi6 rubber is supple enough to not create hot spots on your toe knuckles too. There is also a more breathable tongue and sleeker velcro straps for slight weight savings and better air circulation. The Anasazi Pro also offers a slightly more aggressive and precise fit thanks to a heel which has more tension that the standard Anasazi model.", "Five-Ten Anasazi Pro", 74.33m, "A souped up Anasazi VCS with a Stealth Mi6 toe patch, increased tension in the heel, a more breathable tongue and sleeker velcro straps." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shoes",
                keyColumn: "ShoeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Shoes",
                keyColumn: "ShoeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Shoes",
                keyColumn: "ShoeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Shoes",
                keyColumn: "ShoeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Shoes",
                keyColumn: "ShoeId",
                keyValue: 12);
        }
    }
}

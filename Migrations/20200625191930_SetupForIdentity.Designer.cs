﻿// <auto-generated />
using System;
using ClimbingShoebox.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClimbingShoebox.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200625191930_SetupForIdentity")]
    partial class SetupForIdentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClimbingShoebox.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            BrandId = 1,
                            BrandName = "La Sportiva"
                        },
                        new
                        {
                            BrandId = 2,
                            BrandName = "Scarpa"
                        },
                        new
                        {
                            BrandId = 3,
                            BrandName = "Five-Ten"
                        });
                });

            modelBuilder.Entity("ClimbingShoebox.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Multi-Pitch"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Sport Climbing"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Bouldering"
                        });
                });

            modelBuilder.Entity("ClimbingShoebox.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("OrderPlaced")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OrderTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ClimbingShoebox.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ShoeId")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ShoeId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("ClimbingShoebox.Models.Shoe", b =>
                {
                    b.Property<int>("ShoeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ImageThumbnailUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InStock")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFeaturedShoe")
                        .HasColumnType("bit");

                    b.Property<string>("LongDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShoeId");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Shoes");

                    b.HasData(
                        new
                        {
                            ShoeId = 1,
                            BrandId = 1,
                            CategoryId = 3,
                            ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/la_sportiva_futura.jpg?itok=uFdgfcwB",
                            ImageUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_gallery_full/public/product-images/footwear/la_sportiva_futura.jpg?itok=mk3tuk6k",
                            InStock = true,
                            IsFeaturedShoe = false,
                            LongDescription = "The La Sportiva Futura is an aggressive high end climbing shoe which features the No-Edge construction. No-Edge means that the the traditional edge of the sole is eliminated in favour of a rounded profile, this allows your foot to get closer to the rock than ever before increasing sensitivity and also distributing the load better for a more homogeneous push. The Futura is built on a fairly downturned last which means that they will perform well on overhanging routes with the extra pull which is generated, but they are not so severe that vertical/slab climbing is made difficult. Other details of the Futura include the P3 permanent power platform to maintain the tension and shape of the shoe throughout their life and a fast lacing system which tightens accurately but also lets you get your boots on and off sharpish.",
                            Name = "La Sportiva Futura",
                            Price = 101.25m,
                            ShortDescription = "Top of the range climbing shoe from Sportiva with No-Edge construction to allow your foot to come into closer contact with the rock for greater sensitivity."
                        },
                        new
                        {
                            ShoeId = 2,
                            BrandId = 1,
                            CategoryId = 3,
                            ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/la_sportiva_solution.jpg?itok=Tx5pnWJp",
                            ImageUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_gallery_full/public/product-images/footwear/la_sportiva_solution.jpg?itok=G2m7MQEF",
                            InStock = true,
                            IsFeaturedShoe = false,
                            LongDescription = "The La Sportiva Solution is a revolutionary rock shoe developed specifically for bouldering. Whatever technical problem the rock presents, the “Solution” rock shoe gives you the support you need with the most innovative technology to overcome the boulder move. The upper uses Lock Harness System® technology, which hugs the foot from the inside and combines with the unique deep heel-cups to ensure maximum flexibility when hooking, on incuts and overhangs. The La Sportiva Solution utilises the P3® (Permanent Power Platform) randing system, an active part of the shoe which gives it its versatility and works in synergy with the base of the foot to spread and maintain tension through time. The innovative Fast Lacing System® combined with the tongue in elasticized fabric",
                            Name = "La Sportiva Solution",
                            Price = 90.73m,
                            ShortDescription = "This is the top model from Sportiva. Its superb foot-hugging design has been developed specifically for bouldering."
                        },
                        new
                        {
                            ShoeId = 3,
                            BrandId = 2,
                            CategoryId = 1,
                            ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/untitled-1_0.jpg?itok=kcONkP4K",
                            ImageUrl = "https://cdn.mec.ca/medias/sys_master/high-res/high-res/8938729078814/5053104-STN13.jpg",
                            InStock = true,
                            IsFeaturedShoe = false,
                            LongDescription = "The Scarpa Maestro is the first shoe in Scarpa's new Maestro family, with stiffness, comfort, and performance all primarily designed for hard big wall and multi-pitch climbing. Scarpa have built this shoe from the ground up, creating an entirely new last, stitch pattern, plastic midsole, and active randing system suitable for a technical shoe that can be worn all day. This is Scarpa's most technical performance shoe designed for stiffness and all day comfort.This brand new shoe features a completely original construction.The full length 1.1mm / 1.4mm Talyn midsole provides a high level of stiffness to support the foot for longer climbs, whilst edging, and standing on small crystals, but, to cater for slabs and more featureless climbing, Scarpa have punched three holes in the front section of the midsole to allow the toe area to flex and smear.The IPC - Tensions active randing system is similar to the Bi - Tension system, but it adds a third strip under the sole of the shoe to provide additional strength on edges and micro footholds.This extra rand will also maintain the shoe's performance shape for longer. And, the Vibram XS Edge rubber is actually only half the story, as the heel utitilises the higher friction Grip 2 compound. Not a bad idea as any heel hooks you come across will benefit from this grippier compound.",
                            Name = "Scarpa Maestro",
                            Price = 82.50m,
                            ShortDescription = "An entirely original shoe primarily designed for performance big wall and multi-pitch climbing but will equally do well on sport, trad, and boulder routes, depending on your style and fit."
                        },
                        new
                        {
                            ShoeId = 4,
                            BrandId = 3,
                            CategoryId = 1,
                            ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/osclegacy/five_ten_anasazi_blanco_1.jpg?itok=vDcBJG_c",
                            ImageUrl = "https://shop.cdn.epictv.com/0cnbjeP2TUq6MogetRAp-44923e8eb357c847d2217f5c1ff0736d.jpeg",
                            InStock = true,
                            IsFeaturedShoe = false,
                            LongDescription = "The Five Ten Anasazi Blanco has been revised with a thermoplastic midsole so that it keeps its famous stiffness for longer. A combination of the stiff midsole, high tension heel rand and C4 rubber outsole provides one of the most powerful edging shoes on the market. The lacing system is long to give a precise fit and has been updated to a welded construction for a slicker look.",
                            Name = "Five-Ten Anasazi Blanco",
                            Price = 95.83m,
                            ShortDescription = "The famously stiff anasazi which has been revised with a thermoplastic midsole to keep its rigidity for longer. Combining this with a high tension heel rand produces one of the most powerful edging shoes available."
                        },
                        new
                        {
                            ShoeId = 5,
                            BrandId = 2,
                            CategoryId = 2,
                            ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/osclegacy/Scarpa_Instinct_Lace.jpg?itok=e05Rnm1Q",
                            ImageUrl = "https://shop.epictv.com/sites/default/files/a09a6600c69aac789852759037039727.jpeg",
                            InStock = true,
                            IsFeaturedShoe = false,
                            LongDescription = "The Scarpa Instinct Lace is a great all round performer for sport climbing, bouldering and hard trad routes. The lace system allows for a really accurate fit so your foot placements will be even more precise and power is transfered better through the shoe.This latest evolution of the Instinct lace is far lighter and more sensitive than the old version.The new design features a microfibre upper and 3/4 outsole along with a TPU cage which acts to support the foot and reduce torsional movement and roll during edging.It has also been updated with the same heel cup as the VS, which has proven to be a far better fit for the majority of people.",
                            Name = "Scarpa Instinct Lace",
                            Price = 87.50m,
                            ShortDescription = "Incredibly comfortably for an aggressive high performance shoe the Instinct Lace will perform brilliantly for sport, bouldering and even high end trad routes. The laces allow for a really precise fit and Vibram XS Edge rubber means your accurate foot placements stay where they are."
                        },
                        new
                        {
                            ShoeId = 6,
                            BrandId = 3,
                            CategoryId = 2,
                            ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/product-images/footwear/hiangle_white_side-lateral.jpg?itok=lRP5wBht",
                            ImageUrl = "https://www.bfgcdn.com/1500_1500_90/301-0637-0111/five-ten-hiangle-climbing-shoes.jpg",
                            InStock = true,
                            IsFeaturedShoe = false,
                            LongDescription = "The Hiangle, one of Five Ten’s most popular climbing shoes, has received an update. Built on the same last as the blue Hiangles it now has an unlined microfibre upper for shape retention and stretch rebound, a redesigned heel and increased heel tensioning. The heel is now sensitive Stealth® HF rubber that hugs smears with has increased rand tension to keep heel contact locked down. The new Hiangle is also now made with a split outsole making it more flexible for better performance in competition style indoor climbing and on volumes. The Hiangle keeps it's downturned shape and extended toe rubber for secure toe hooks.",
                            Name = "Five-Ten Hiangle",
                            Price = 74.17m,
                            ShortDescription = "Stiff, comfortable downturned shoe that is great for tiny edges and steep overhanging routes."
                        },
                        new
                        {
                            ShoeId = 7,
                            BrandId = 2,
                            CategoryId = 3,
                            ImageThumbnailUrl = "https://www.bananafingers.co.uk/sites/default/files/styles/product_teaser/public/osclegacy/Scarpa_Instinct_VSR.jpg?itok=xOczMULQ",
                            ImageUrl = "https://shop.cdn.epictv.com/FV7D0lhnRWWhADXL2OLw-917c326a874c1f255002a0fb7e4ec25d.jpeg",
                            InStock = true,
                            IsFeaturedShoe = true,
                            LongDescription = "The Scarpa Instinct VS-R is essentially a softer version of the incredibly popular Instinct VS, it uses the same last and rand as the VS for high levels of sensitivity and accurate foot placements. The VS-R has a semi-aggressive profile will work well at any angle but will still allow you to pull in on holds when on overhanging terrain.The use of a softer mid-sole and Vibram XS Grip rubber makes for a more all round round performance rather than being a bit more biased towards edging like the Instinct VS and also makes them more sensitive.",
                            Name = "Scarpa Instinct VS-R",
                            Price = 91.25m,
                            ShortDescription = "A softer version of the highly popular Instinct VS with Vibram XS Grip rubber rather than XS Edge for a greater all round performance (rather than being tailored towards edging like the VS."
                        });
                });

            modelBuilder.Entity("ClimbingShoebox.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("ShoppingCartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("ShoeId")
                        .HasColumnType("int");

                    b.Property<string>("ShoppingCartId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShoppingCartItemId");

                    b.HasIndex("ShoeId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ClimbingShoebox.Models.OrderDetail", b =>
                {
                    b.HasOne("ClimbingShoebox.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClimbingShoebox.Models.Shoe", "Shoe")
                        .WithMany()
                        .HasForeignKey("ShoeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClimbingShoebox.Models.Shoe", b =>
                {
                    b.HasOne("ClimbingShoebox.Models.Brand", "Brand")
                        .WithMany("ShoesInBrand")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClimbingShoebox.Models.Category", "Category")
                        .WithMany("ShoesInCat")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClimbingShoebox.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("ClimbingShoebox.Models.Shoe", "Shoe")
                        .WithMany()
                        .HasForeignKey("ShoeId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

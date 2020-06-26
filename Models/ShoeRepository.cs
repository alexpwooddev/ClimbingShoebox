﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public class ShoeRepository : IShoeRepository
    {
        private readonly AppDbContext appDbContext;

        public ShoeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        
        public IEnumerable<Shoe> AllShoes
        {
            get
            {
                return appDbContext.Shoes.Include(c => c.Category).Include(b => b.Brand);
            }
        }

        public Shoe FeaturedShoe
        {
            get
            {
                return appDbContext.Shoes.FirstOrDefault(s => s.IsFeaturedShoe == true);
            }
        }

        public Shoe GetShoebyId(int shoeId)
        {
            return appDbContext.Shoes.FirstOrDefault(s => s.ShoeId == shoeId);
        }

        public IEnumerable<Shoe> GetShoesByName(string nameQuery)
        {
            var queryList = from s in appDbContext.Shoes
                            where s.Name.Contains(nameQuery) || string.IsNullOrEmpty(nameQuery)
                            orderby s.Name
                            select s;
            return queryList;
        }
    }
}

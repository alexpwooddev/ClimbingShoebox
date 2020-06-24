using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> AllBrands { get; }
    }
}

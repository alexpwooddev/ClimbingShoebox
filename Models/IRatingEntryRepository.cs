using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimbingShoebox.Models
{
    public interface IRatingEntryRepository
    {
        
        IEnumerable<RatingEntry> AllRatings { get; }

        IEnumerable<int> GetShoeRatings (int shoeId);

        bool AddShoeRating(IServiceProvider services, int shoeId, int rating);
    }
}

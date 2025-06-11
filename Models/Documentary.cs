using MovieCollection.Core.Enums;

namespace MovieCollection.Core.Models
{
    public class Documentary : Movie
    {
        public override Genre GetGenre()
        {
            return Genre.documentary;
        }

        public override string GetSummary()
        {
            return $"Documentary: {Title} - Released in: {ReleaseYear} - directed by {Director}. It is {Duration} minutes long.";
        }
    }
}

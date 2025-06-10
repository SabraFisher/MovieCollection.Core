using MovieCollection.Core.Enums;

namespace MovieCollection.Core.Models
{
    public class Documentary : Movie
    {
        public override Genre GetGenre()
        {
            return Genre.Documentary;
        }

        public override string GetSummary()
        {
            return $"{Title} is a Documentary released in {ReleaseYear} directed by {Director}. It is {Duration} minutes long.";
        }
    }
}

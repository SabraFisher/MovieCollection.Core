using MovieCollection.Core.Enums;

namespace MovieCollection.Core.Models
{
    public class FeatureFilm : Movie
    {
        public override Genre GetGenre()
        {
            return Genre.FeatureFilm;
        }

        public override string GetSummary()
        {
            return $"{Title} is a feature film directed by {Director}, released in {ReleaseYear}.";
        }
    }
}

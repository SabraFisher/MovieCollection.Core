using MovieCollection.Core.Enums;

namespace MovieCollection.Core.Models
{
    public class FeatureFilm : Movie
    {
        public override Genre GetGenre()
        {
            return Genre.featurefilm;
        }

        public override string GetSummary()
        {
            return $"Feature Film: {Title} - Released in: {ReleaseYear} - directed by {Director}. It is {Duration} minutes long.";
        }
    }
}

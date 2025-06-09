using MovieCollection.Core.Enums;

namespace MovieCollection.Core.Models
{
    public class Documentary : Movie
    {
        public override Category GetCategory()
        {
            return Category.Documentary;
        }

        public override string GetSummary()
        {
            return $"{Title} is a documentary directed by {Director}, released in {ReleaseYear}.";
        }
    }
}

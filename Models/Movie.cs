using MovieCollection.Core.Enums;

namespace MovieCollection.Core.Models
{
    public abstract class Movie
    {
        public string? Title { get; set; }
        public Genre Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string? Director { get; set; }
        public Reviews Reviews { get; set; }
        public Ratings Ratings { get; set; }
        public int Duration { get; set; }

        public abstract string GetSummary();

        public virtual Genre GetGenre()
        {
            return Genre.Unknown;
        }
    }
}

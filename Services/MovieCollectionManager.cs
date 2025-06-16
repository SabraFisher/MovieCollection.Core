using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieCollection.Core.Enums;
using MovieCollection.Core.Models;

namespace MovieCollection.Core.Services
{
    public class MovieCollectionManager
    {
        private readonly List<Movie> _movieCollection = new ();
        public List<Movie> MovieCollection => _movieCollection;
        public MovieCollectionManager() 
        {
            Movie[] movies = LoadMoviesArrayFromFile();
            if (movies != null && movies.Length > 0)
            {
                _movieCollection.AddRange(movies);
            }
            else
            {
                _movieCollection.AddRange(CreateSampleMovieArray());
            }
        }

        public static Movie[] CreateSampleMovieArray()
        {
            return
            [
                new FeatureFilm
                {
                    Title = "The Matrix",
                    Genre = Genre.featurefilm,
                    ReleaseYear = 1999,
                    Director = "Lana & Lilly Wachowski",
                    Duration = 136
                },
                new Documentary
                {
                    Title = "Planet Earth",
                    Genre = Genre.documentary,
                    ReleaseYear = 2006,
                    Director = "Alastair Fothergill",
                    Duration = 60
                },
                new FeatureFilm
                {
                    Title = "Inception",
                    Genre = Genre.featurefilm,
                    ReleaseYear = 2010,
                    Director = "Christopher Nolan",
                    Duration = 148
                }
            ];
        }

        public static Movie[] LoadMoviesArrayFromFile()
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, "movieCollectionArrayFile.txt");
            string filePath = Path.GetFullPath(sFile);
            Console.WriteLine($"\nLoading movies from file: {filePath}...");
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Movie collection file not found. Using sample movies.");
                return CreateSampleMovieArray();
            }

            string[] loadedLines = File.ReadAllLines(filePath);
            if (loadedLines == null || loadedLines.Length == 0)
            {
                Console.WriteLine("Movie file is empty. Using sample movies.");
                return CreateSampleMovieArray();
            }

            var movies = new Movie[loadedLines.Length];
            for (int i = 0; i < loadedLines.Length; i++)
            {
                string[] parts = loadedLines[i].Split(',');
                if (parts.Length >= 5 && Enum.TryParse(parts[1], true, out Genre genre))
                {
                    bool isReleaseYearParsed = int.TryParse(parts[2], out int releaseYear);
                    bool isDurationParsed = int.TryParse(parts[4], out int duration);

                    switch (genre)
                    {
                        case Genre.featurefilm:
                            movies[i] = new FeatureFilm
                            {
                                Title = parts[0],
                                Genre = Genre.featurefilm,
                                ReleaseYear = isReleaseYearParsed ? releaseYear : 0,
                                Director = parts[3],
                                Duration = isDurationParsed ? duration : 0
                            };
                            break;

                        case Genre.documentary:
                            movies[i] = new Documentary
                            {
                                Title = parts[0],
                                Genre = Genre.documentary,
                                ReleaseYear = isReleaseYearParsed ? releaseYear : 0,
                                Director = parts[3],
                                Duration = isDurationParsed ? duration : 0
                            };
                            break;

                        default:
                            movies[i] = new Unknown
                            {
                                Title = parts[0],
                                Genre = Genre.unknown,
                                ReleaseYear = isReleaseYearParsed ? releaseYear : 0,
                                Director = parts[3],
                                Duration = isDurationParsed ? duration : 0
                            };
                            break;
                    }
                }
                else
                {
                    movies[i] = new Unknown
                    {
                        Title = parts.Length > 0 ? parts[0] : "Unknown",
                        Genre = Genre.unknown,
                        ReleaseYear = parts.Length > 2 && int.TryParse(parts[2], out int releaseYear) ? releaseYear : 0,
                        Director = parts.Length > 3 ? parts[3] : "Unknown",
                        Duration = parts.Length > 4 && int.TryParse(parts[4], out int duration) ? duration : 0
                    };
                }
            }
            return movies;
        }

        public static void SelectionSortByTitle(List<Movie> MovieCollection)
        {
            for (int i = 0; i < MovieCollection.Count - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < MovieCollection.Count; j++)
                {
                    // Ensure Title is not null before calling Trim() or ToLower()
                    string titleJ = MovieCollection[j].Title?.Trim().ToLower() ?? string.Empty;
                    string titleMinIndex = MovieCollection[minIndex].Title?.Trim().ToLower() ?? string.Empty;

                    if (string.Compare(titleJ, titleMinIndex) < 0)
                    {
                        minIndex = j;
                    }
                }
                // Swap the found minimum element with the first element
                if (minIndex != i)
                {
                    var temp = MovieCollection[i];
                    MovieCollection[i] = MovieCollection[minIndex];
                    MovieCollection[minIndex] = temp;
                }
            }
        }

        public static Movie[] ConvertListToArray(List<Movie> MovieCollection, out Movie[] movies)
        {
            if (MovieCollection == null || MovieCollection.Count == 0)
            {
                movies = []; // Return an empty array if the collection is null or empty
            }
            else
            {
                movies = [.. MovieCollection]; // Convert the list to an array
            }
            return movies;
        }

        public static void SaveToFile(Movie[] movies)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, "movieCollectionArrayFile.txt");
            string filePath = Path.GetFullPath(sFile);
            using (StreamWriter writer = new(filePath))
            {
                foreach (Movie movie in movies)
                {
                    writer.WriteLine($"{movie.Title},{movie.GetGenre()},{movie.ReleaseYear},{movie.Director},{movie.Duration}");
                }
            }
            Console.WriteLine("\nMovies saved successfully!");
        }
    }
}

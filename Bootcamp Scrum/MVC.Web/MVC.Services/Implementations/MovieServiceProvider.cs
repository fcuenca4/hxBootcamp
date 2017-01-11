using MVC.Data.Repositories;
using MVC.Entities;
using MVC.Services.Interface;

namespace MVC.Services.Implementations
{
    public class MovieServiceProvider : ServiceProvider<Movie>, IMovieService
    {
        public MovieServiceProvider(MovieRepository repo) : base(repo)
        {
        }
        
    }
}

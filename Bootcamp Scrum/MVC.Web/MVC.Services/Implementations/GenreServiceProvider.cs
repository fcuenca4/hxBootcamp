using MVC.Data.Repositories;
using MVC.Entities;
using MVC.Services.Interface;

namespace MVC.Services.Implementations
{
    public class GenreServiceProvider : ServiceProvider<Genre>, IGenreService
    {
        public GenreServiceProvider(GenreRepository repo) : base(repo)
        {
        }
    }
}

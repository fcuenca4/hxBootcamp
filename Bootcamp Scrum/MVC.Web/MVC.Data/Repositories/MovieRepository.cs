using MVC.Entities;
namespace MVC.Data.Repositories
{
    public class MovieRepository : RepositoryBase<Movie>
    {
        public MovieRepository(MoviesContext dbContext) : base(dbContext)
        {
        }
    }
}

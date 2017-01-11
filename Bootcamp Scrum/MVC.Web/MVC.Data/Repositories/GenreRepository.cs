using MVC.Entities;
namespace MVC.Data.Repositories
{
    public class GenreRepository : RepositoryBase<Genre>
    {
        public GenreRepository(MoviesContext dbContext) : base(dbContext)
        {
        }
    }
}

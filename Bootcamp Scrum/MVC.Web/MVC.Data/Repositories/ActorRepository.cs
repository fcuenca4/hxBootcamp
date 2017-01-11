using MVC.Entities;
namespace MVC.Data.Repositories
{
    public class ActorRepository:RepositoryBase<Actor> 
    {
        public ActorRepository(MoviesContext dbContext) : base(dbContext)
        {
        }

    }
}

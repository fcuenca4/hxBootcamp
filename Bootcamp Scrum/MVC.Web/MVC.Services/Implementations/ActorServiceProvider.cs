using MVC.Data.Repositories;
using MVC.Entities;
using MVC.Services.Interface;

namespace MVC.Services.Implementations
{
    public class ActorServiceProvider : ServiceProvider<Actor>, IActorService
    {
        public ActorServiceProvider(ActorRepository repo) : base(repo)
        {
        }
    }
}

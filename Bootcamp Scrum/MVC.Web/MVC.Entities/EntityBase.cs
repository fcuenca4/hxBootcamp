using System;

namespace MVC.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}

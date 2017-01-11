using MVC.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace MVC.Data
{
    public class MoviesContext : DbContext
    {
        public MoviesContext() : base("MoviesDB")
        {

        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .Property(g => g.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Genre>()
                .Property(g => g.Name)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute { IsUnique = true }))
                .HasMaxLength(40);

            modelBuilder.Entity<Actor>()
                .Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Actor>()
                .Property(a => a.Name)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute { IsUnique = true }))
                .HasMaxLength(50);
            modelBuilder.Entity<Actor>()
                .Property(a => a.UrlBiography)
                .IsOptional()
                .HasMaxLength(150);

            modelBuilder.Entity<Movie>()
                .Property(m => m.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Movie>()
                .Property(m => m.Name)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute { IsUnique = true }))
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Movie>()
                .Property(m => m.Image)
                .IsOptional()
                .HasMaxLength(50);
            modelBuilder.Entity<Movie>()
                .Property(m => m.ReleaseDate)
                .IsRequired();
            modelBuilder.Entity<Movie>()
                .Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(2048);
            modelBuilder.Entity<Movie>()
                .Property(m => m.Duration)
                .IsOptional();
            modelBuilder.Entity<Movie>()
               .HasMany<Actor>(m => m.Actors)
               .WithMany()
               .Map(cs =>
               {
                   cs.MapLeftKey("MovieId");
                   cs.MapRightKey("ActorId");
                   cs.ToTable("MovieActor");
               });
            modelBuilder.Entity<Movie>()
               .HasMany<Genre>(m => m.Genres)
               .WithMany()
               .Map(cs =>
               {
                   cs.MapLeftKey("MovieId");
                   cs.MapRightKey("GenreId");
                   cs.ToTable("MovieGenre");
               });
        }
    }
}

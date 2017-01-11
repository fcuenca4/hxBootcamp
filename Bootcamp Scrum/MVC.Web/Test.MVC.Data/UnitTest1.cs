
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC.Entities;
using MVC.Data;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using MVC.Data.Repositories;
using MVC.Services.Interface;
using MVC.Services.Implementations;

namespace Test.MVC.Data
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        [TestMethod]
        public void TestMethod2()
        {
            MoviesContext m = new MoviesContext();
            IMovieService ms = new MovieServiceProvider(new MovieRepository(m));
            foreach (var item in ms.GetAllElements())
            {
                item.ToString();
            }
        }
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                new MoviesContext().Database.Delete();
                new MoviesContext().Database.Create();
                using (var moviesContext = new MoviesContext())
                {

                List<Genre> genres = new List<Genre> {
                new Genre {Name="Adolecencia" },
                new Genre {Name="Comedia" },
                new Genre {Name="Familiar" },
                new Genre {Name="Romance" }
            };
                    List<Actor> actors = new List<Actor> {
                new Actor {Name="Lindsay Lohan", UrlBiography="es.wikipedia.org/wiki/Lindsay_Lohan" },
                new Actor {Name="Dennis Quaid",UrlBiography="es.wikipedia.org/wiki/Dennis_Quaid" }
            };
                    List<Movie> movies = new List<Movie> {
                new Movie {Name = "The Parent Trap", Description ="Hallie y Annie, dos gemelas que se parecen como dos gotas de agua, fueron separadas poco después de nacer a causa del divorcio de sus padres. Hallie vive en Napa Valley con su padre, que es viticultor. Por su parte, Annie reside en Londres con su madre, una famosa diseñadora de trajes de novia. Ninguna de las dos conoce la existencia de la otra, pero el destino hace que se encuentren por casualidad en Maine, en un campamento de verano. Antes de volver a casa, las niñas urden un plan para conseguir reunir a sus padres, pero hay un inconveniente con el que no contaban: su padre planea casarse con otra mujer.", Duration = 120, ReleaseDate= new DateTime(1998,8,31),
                Image="001.jpg", Actors = actors, Genres = genres}
            };

                    MovieRepository repo = new MovieRepository(moviesContext);
                    foreach (var g in genres)
                        moviesContext.Genres.Add(g);
                    foreach (var a in actors)
                        moviesContext.Actors.Add(a);
                    moviesContext.SaveChanges();
                    foreach (var p in movies)
                        repo.Insert(p);
                    moviesContext.SaveChanges();
                }
                
            }
            catch(System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var failure in e.EntityValidationErrors)
                {
                    string validationErrors = "";

                    foreach (var error in failure.ValidationErrors)
                    {
                        validationErrors += error.PropertyName + "  " + error.ErrorMessage;
                    }
                    Debug.Print(validationErrors);
                }
            }
            
        }
    }
}
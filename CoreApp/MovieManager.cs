using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace CoreApp
{
    public class MovieManager : BaseManager
    {

        /*
         * Metodo para la creacion de una pelicula
         * Valida que el título de la película no exista en la base de datos
         */
        public void Create(Movie movie)
        {
            try
            {
                var mCrud = new MovieCrudFactory();
                var mExist = mCrud.RetrieveByTitle<Movie>(movie);

                if (mExist == null)
                {
                    mCrud.Create(movie);

                    var uCrud = new UsersCrudFactory();
                    var users = uCrud.RetrieveAll<User>();

                    var emailManager = new EmailManager();
                    emailManager.SendNewMovie(movie.Title, users).GetAwaiter().GetResult();
                }
                else
                {
                    throw new Exception("El título de la película ya existe. Por favor, ingrese otro título.");
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
            }
        }

        public Movie Update(Movie movie)
        {
            try
            {
                var mCrud = new MovieCrudFactory();
                var mExist = mCrud.RetrieveById<Movie>(movie.Id);
                if (mExist != null)
                {
                    mCrud.Update(movie);
                    return movie;
                }
                else
                {
                    throw new Exception("La pelicula no existe en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
                return null; // En caso de error, retornamos null
            }
        }

        public Movie Delete(Movie movie)
        {
            try
            {
                var mCrud = new MovieCrudFactory();
                var mExist = mCrud.RetrieveById<Movie>(movie.Id);
                if (mExist != null)
                {
                    mCrud.Delete(movie);
                    return movie;
                }
                else
                {
                    throw new Exception("La pelicula no existe en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
                return null; // En caso de error, retornamos null
            }
        }

        public List<Movie> RetrieveAll()
        {
            var mCrud = new MovieCrudFactory();
            return mCrud.RetrieveAll<Movie>();
        }

        public Movie RetrieveById(Movie movie)
        {
            var mCrud = new MovieCrudFactory();
            return mCrud.RetrieveById<Movie>(movie.Id);
        }
    }
}

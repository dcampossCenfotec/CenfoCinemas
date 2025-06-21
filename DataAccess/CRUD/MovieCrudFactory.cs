using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class MovieCrudFactory : CrudFactory
    {
        public MovieCrudFactory()
        {
            _sqlDao = DAO.SqlDAO.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var movie = baseDTO as Movie;
            var sqlOperation = new DAO.SqlOperation
            {
                ProcedureName = "CRE_MOVIE_PR"
            };
            
            sqlOperation.AddIntParam("P_Id", movie.Id);
            sqlOperation.AddStringParameter("P_Title", movie.Title);
            sqlOperation.AddStringParameter("P_Description", movie.Description);
            sqlOperation.AddStringParameter("P_Director", movie.Director);
            sqlOperation.AddDateTimeParam("P_ReleaseDate", movie.ReleaseDate);
            sqlOperation.AddStringParameter("P_Genre", movie.Genre);
            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var movie = baseDTO as Movie;
            var sqlOperation = new DAO.SqlOperation
            {
                ProcedureName = "UPD_MOVIE_PR"
            };
            sqlOperation.AddIntParam("P_Id", movie.Id);
            sqlOperation.AddStringParameter("P_Title", movie.Title);
            sqlOperation.AddStringParameter("P_Description", movie.Description);
            sqlOperation.AddStringParameter("P_Director", movie.Director);
            sqlOperation.AddDateTimeParam("P_ReleaseDate", movie.ReleaseDate);
            sqlOperation.AddStringParameter("P_Genre", movie.Genre);
            _sqlDao.ExecuteProcedure(sqlOperation);

        }
        public override void Delete(BaseDTO baseDTO)
        {
            var movie = baseDTO as Movie;
            var sqlOperation = new DAO.SqlOperation
            {
                ProcedureName = "DEL_MOVIE_PR"
            };

            sqlOperation.AddIntParam("P_Id", movie.Id);

            _sqlDao.ExecuteProcedure(sqlOperation);
        }
        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }
        public override T RetrieveById<T>(int id)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_MOVIE_BY_ID_PR" };

            sqlOperation.AddIntParam("P_Id", id);

            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                var movie = BuildMovie(lstResults[0]);
                return (T)Convert.ChangeType(movie, typeof(T));
            }

            return default(T);
        }

        public T RetrieveByTitle<T>(Movie movie)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_MOVIE_BY_TITLE_PR" };

            sqlOperation.AddStringParameter("P_Title", movie.Title);

            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                var row = lstResults[0];
                movie = BuildMovie(row);
                return (T)Convert.ChangeType(movie, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstUsers = new List<T>();
            var sqlOperation = new DAO.SqlOperation
            {
                ProcedureName = "RET_ALL_MOVIES_PR"
            };
            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var user = BuildMovie(row);
                    lstUsers.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstUsers;
        }

        //Metodo de convierte el diccionario en un User
        private Movie BuildMovie(Dictionary<string, object> row)
        {
            return new Movie
            {
                Id = (int)row["Id"],
                Created = Convert.ToDateTime(row["Created"]),
                //Updated = Convert.ToDateTime(row["Updated"]),
                Title = row["Title"].ToString(),
                Description = row["Description"].ToString(),
                Genre = row["Genre"].ToString(),
                ReleaseDate = Convert.ToDateTime(row["ReleaseDate"]),
                Director = row["Director"].ToString()
            };
        }

    }
}

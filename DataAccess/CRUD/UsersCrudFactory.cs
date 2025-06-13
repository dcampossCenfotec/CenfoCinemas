using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class UsersCrudFactory : CrudFactory
    {
        public UsersCrudFactory()
        {
            _sqlDao = DAO.SqlDAO.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var user = baseDTO as User;

            var sqlOperation = new DAO.SqlOperation
            {
                ProcedureName = "CRE_USER_PR"
            };

            sqlOperation.AddStringParameter("P_UserCode", user.UserCode);
            sqlOperation.AddStringParameter("P_Name", user.Name);
            sqlOperation.AddStringParameter("P_Email", user.Email);
            sqlOperation.AddStringParameter("P_Password", user.Password);
            sqlOperation.AddDateTimeParam("P_BirthDate", user.Birth);
            sqlOperation.AddStringParameter("P_Status", user.Status);

            _sqlDao.ExecuteProcedure(sqlOperation);
        }
        public override void Update(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }
        public override void Delete(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }
        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }
        public override T RetrieveById<T>()
        {
            throw new NotImplementedException();
        }
        public override List<T> RetrieveAll<T>()
        {
            var lstUsers = new List<T>();
            var sqlOperation = new DAO.SqlOperation
            {
                ProcedureName = "RET_ALL_USERS_PR"
            };
            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var user = BuildUser(row);
                    lstUsers.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return lstUsers;
        }

        //Metodo de convierte el diccionario en un User
        private User BuildUser(Dictionary<string, object> row)
        {
            return new User
            {
                Id = (int)row["Id"],
                Created = Convert.ToDateTime(row["Created"]),
                //Updated = Convert.ToDateTime(row["Updated"]),
                UserCode = row["UserCode"].ToString(),
                Name = row["Name"].ToString(),
                Email = row["Email"].ToString(),
                Password = row["Password"].ToString(),
                Birth = Convert.ToDateTime(row["BirthDate"]),
                Status = row["Status"].ToString()
            };
        }
    }
}

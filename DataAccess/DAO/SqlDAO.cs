using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class SqlDAO
    {
        private static SqlDAO _instance;
        private string _connectionString;

        private SqlDAO() {
            _connectionString = string.Empty;
        }

        public static SqlDAO GetInstance() {
            if (_instance == null)
            {
                _instance = new SqlDAO();
            }
            return _instance;
        }

        public void ExecuteProcedure(SqlOperation operation)
        {
            using (var conn = new SqlConnection(_connectionString)) 
            {
                using (var command = new SqlCommand(operation.ProcedureName, conn) 
                { 
                    CommandType = System.Data.CommandType.StoredProcedure
                }) 
                {
                    //set de los params
                    foreach (var param in operation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    //exec SP
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation operation) {
            var list = new List<Dictionary<string, object>>();
            return list;
        }
    }
}
